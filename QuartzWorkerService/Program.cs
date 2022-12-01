using Quartz.Spi;
using QuartzWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<HelloJob>();
        services.AddTransient<IJobFactory, JobFactory>();
    })
    .Build();

await host.RunAsync();
