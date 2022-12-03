using Quartz.Spi;
using QuartzWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<HelloJob>();
        services.AddTransient<IJobFactory, JobFactory>();
        services.AddTransient<IPrintTime, PrintTime>();
    })
    .Build();

await host.RunAsync();
