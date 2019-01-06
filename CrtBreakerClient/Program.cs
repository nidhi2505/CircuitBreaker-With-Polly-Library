using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CrtBreakerClient
{
    public class Program
    {
        public static int count = 0;
        static void Main(string[] args)
        {
            var cbreaker = new ExecutionPolicy();
            var config = new ExecutionConfiguration();
            config.BreakDuration = TimeSpan.FromSeconds(3);

            //trying with Invalid URI as below
            var uri = "http://localhost:53393/apii/values";
            config.Endpoint = uri;
            config.Retries = 3;
            config.Timeout = TimeSpan.FromSeconds(10);

          cbreaker.ExecuteAsync<HttpResponseMessage>( () => Task.Run(()=> CallAPI()), config);
            Thread.Sleep(10000);
            cbreaker.ExecuteAsync<HttpResponseMessage>(() => Task.Run(() => CallAPI()), config);
            Thread.Sleep(10000);
            cbreaker.ExecuteAsync<HttpResponseMessage>(() => Task.Run(() => CallAPI()), config);
            Console.ReadLine();
        }

        public  static HttpResponseMessage CallAPI()
        {
            var client = new HttpClient();
            var uri = "http://localhost:53393/apii/values";
            Console.WriteLine($"Executed  " + count);
            count = count + 1;
            // call API
            var result =  Task.Run(() => client.GetAsync(uri)).Result;

            return result;

        }
        public async static Task<HttpResponseMessage> CallAPIAsync()
        {
            var client = new HttpClient();
            var uri = "http://localhost:53393/apii/values";
            Console.WriteLine($"Executed  " + count);
            count = count + 1;
            // call API
            var result = await client.GetAsync(uri);
               
                return result;
         
        }
    }
    
}
