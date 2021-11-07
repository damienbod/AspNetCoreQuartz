using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace AspNetCoreQuartz.QuartzServices
{
    [DisallowConcurrentExecution]
    public class NonConconcurrentJob : IJob
    {
        private readonly ILogger<NonConconcurrentJob> _logger;
        private static int _counter = 0;    
        public NonConconcurrentJob(ILogger<NonConconcurrentJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Running NonConconcurrentJob {_counter++}");
            return Task.CompletedTask;
        }
    }
}



