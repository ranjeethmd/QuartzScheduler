using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzWorkerService
{
    internal class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _service;
        private readonly ConcurrentDictionary<IJob,IServiceScope> _jobScopesMap = new();
        public JobFactory(IServiceProvider serviceProvider) {
            _service = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;
            var scope = _service.CreateScope();

            var job = (IJob)scope.ServiceProvider.GetRequiredService(jobType);
            _jobScopesMap.TryAdd(job, scope);

            return job;
        }

        public void ReturnJob(IJob job)
        {
            if(_jobScopesMap.TryGetValue(job, out var scope))
            {
                scope.Dispose();
                _jobScopesMap.TryRemove(job, out _);
            }            
        }
    }
}
