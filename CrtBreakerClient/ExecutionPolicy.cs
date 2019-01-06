using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CrtBreakerClient;
using Polly;
using Polly.Timeout;
using Polly.Wrap;

namespace CrtBreakerClient
{

    public class ExecutionPolicy 
    {
        public Dictionary<string, IPolicyWrap> Policies = new Dictionary<string, IPolicyWrap>();

    
        public Task<T> ExecuteAsync<T>(Func<Task<T>> func, ExecutionConfiguration config)
        {
            if (!Policies.TryGetValue(config.Endpoint, out var policy))
            {
                policy = WaitAndRetryPolicy<T>(config)
                    .WrapAsync(CircuitBreakerPolicy<T>(config)
                    .WrapAsync(TimeoutPolicy(config)));
                Policies.Add(config.Endpoint, policy);
            }

            return ((PolicyWrap<T>)policy).ExecuteAsync(func);
        }

        private IAsyncPolicy TimeoutPolicy(ExecutionConfiguration config)
            => Policy.TimeoutAsync(config.Timeout, TimeoutStrategy.Pessimistic, (context, timespan, task) =>
               {
                   Console.WriteLine($"CIRCUIT BREAKER TIMEOUT after {timespan.TotalSeconds} seconds");
                   task.ContinueWith(t =>
                   {
                       if (t.IsFaulted)
                           Console.WriteLine($"Task '{config.Endpoint}' faulted", t.Exception);
                   });
                   return Task.CompletedTask;
               });

        private IAsyncPolicy<T> CircuitBreakerPolicy<T>(ExecutionConfiguration config)
        {
            var policyBuilder = Policy<T>.Handle<Exception>().Or<TimeoutRejectedException>();
            if (typeof(T) == typeof(HttpResponseMessage))
                policyBuilder = policyBuilder.OrResult(res => (int)(res as HttpResponseMessage).StatusCode >= 400);
            return policyBuilder.CircuitBreakerAsync(
                //config.Retries,
                1,
                config.BreakDuration,
                (exception, timespan, context) =>
                {
                    Console.WriteLine($"CIRCUIT BREAKER OPEN: for the endpoint '{config.Endpoint}'");
                },
                context =>
                {
                    Console.WriteLine($"CIRCUIT BREAKER CLOSE:for the endpoint '{config.Endpoint}'");
                });
        }

        private IAsyncPolicy<T> WaitAndRetryPolicy<T>(ExecutionConfiguration config)
        {
            var policyBuilder = Policy<T>.Handle<Exception>().Or<TimeoutRejectedException>();
            if (typeof(T) == typeof(HttpResponseMessage))
                policyBuilder = policyBuilder.OrResult(res => (int)(res as HttpResponseMessage).StatusCode >= 400);
            return policyBuilder.WaitAndRetryAsync(RetriesTimeSpan(config.Retries),(exception, tipeSpan, retry, context) =>
               Console.WriteLine($"CIRCUIT BREAKER RETRY -> retry number {retry} "));
        }

        private IEnumerable<TimeSpan> RetriesTimeSpan(int retries)
        {
            for (var i = 0; i < retries; i++)
                yield return TimeSpan.FromMilliseconds(300);
        }

    }

}