using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace AspNetCoreQuartz.QuartzServices
{
    [DisallowConcurrentExecution]
    public class ConconcurrentJob : IJob
    {
        private readonly ILogger<ConconcurrentJob> _logger;
        private static int _counter = 0;
        public ConconcurrentJob(ILogger<ConconcurrentJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Running Conconcurrent Job {_counter++}");
            return Task.CompletedTask;
        }
    }
}



