using Quartz;
using System.Diagnostics;

namespace QuartzWorkerService
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    internal class HelloJob : IJob
    {
        private readonly ILogger<HelloJob> _logger;
        private readonly IPrintTime _printTime;

        public HelloJob(ILogger<HelloJob> logger, IPrintTime printTime)
        {
            _logger = logger;
            _printTime = printTime;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();

            try
            {
                _logger.LogInformation($"Job - {GetType()} startes at {_printTime.GetCurrentTime()}");

                watch.Start();

                _logger.LogInformation("Greetings from Hello Job!");

                await Task.CompletedTask;
            }
            catch (Exception ex)
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
