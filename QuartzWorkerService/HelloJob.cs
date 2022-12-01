using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzWorkerService
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    internal class HelloJob:IJob
    {
        private readonly ILogger<HelloJob> _logger;

        public HelloJob(ILogger<HelloJob> logger)
        {
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();

            try
            {
                _logger.LogInformation($"Job - {GetType()} startes at {DateTime.Now}");

                watch.Start();

                _logger.LogInformation("Greetings from Hello Job!");

                await Task.CompletedTask;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error while running the job - {GetType()}");
            }
            finally
            {
                watch.Stop();
                _logger.LogInformation($"Total execution time of the job {GetType()} in milliseconds - {watch.ElapsedMilliseconds}");
            }
        }
    }
}
