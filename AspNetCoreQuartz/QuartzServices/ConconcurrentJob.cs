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
            var count = _counter++;
            _logger.LogInformation($"Conconcurrent Job BEGIN {count} {DateTime.UtcNow}");
            Thread.Sleep(7);
            _logger.LogInformation($"Conconcurrent Job END {count} {DateTime.UtcNow}");

            return Task.CompletedTask;
        }
    }
}



