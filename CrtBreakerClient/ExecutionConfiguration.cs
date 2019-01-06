using System;

namespace CrtBreakerClient
{
    public class ExecutionConfiguration
    {
        public string Endpoint { get; set; }
        public int Retries { get; set; }
        public TimeSpan BreakDuration { get; set; }
        public TimeSpan Timeout { get; set; }
    }

}