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

        public async Task Execute(IJobExecutionContext context)
        {
            await DoWork();
            return;
        }

        public async Task DoWork()
        {
            var count = _counter++;
            _logger.LogInformation($"NonConconcurrentJob Job BEGIN {count} {DateTime.UtcNow}");
            await Task.Delay(10);
            _logger.LogInformation($"NonConconcurrentJob Job END {count} {DateTime.UtcNow}");

            return;
        }
    }
}



