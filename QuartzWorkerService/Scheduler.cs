using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace QuartzWorkerService
{
    public class Scheduler : BackgroundService
    {
        private readonly ILogger<Scheduler> _logger;
        private readonly IJobFactory _jobFactory;
        private readonly IConfiguration _configuration;

        public Scheduler(ILogger<Scheduler> logger, IJobFactory factory, IConfiguration configuration)
        {
            _logger = logger;
            _jobFactory = factory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Begin setting up Scheduler");

            var taskCompletion = new TaskCompletionSource();

            string jobName = _configuration.GetValue<string>("Job:JobName");
            string groupName = _configuration.GetValue<string>("Job:GroupName");
            string triggerName = _configuration.GetValue<string>("Job:TriggerName");
            string cronExpression = _configuration.GetValue<string>("Job:CronExpression");



            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            scheduler.JobFactory = _jobFactory;

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<HelloJob>()
                          .WithIdentity(jobName, groupName)
                          .Build();

            ITrigger trigger = TriggerBuilder.Create()
                            .WithIdentity(triggerName, groupName)
                            .WithCronSchedule(cronExpression, x => x.WithMisfireHandlingInstructionFireAndProceed())
                            .ForJob(job)
                            .Build();

            await scheduler.ScheduleJob(job, trigger);

            _logger.LogInformation("completed setting up Scheduler");

            await taskCompletion.Task.WaitAsync(stoppingToken);


            await scheduler.Shutdown();

            _logger.LogInformation("sutting down scheduler");

        }
    }
}