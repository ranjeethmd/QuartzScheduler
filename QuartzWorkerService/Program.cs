using Quartz.Spi;
using QuartzWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Scheduler>();
        services.AddScoped<HelloJob>();
        services.AddTransient<IJobFactory, JobFactory>();
        services.AddTransient<IPrintTime, PrintTime>();
    })
    .Build();

await host.RunAsync();
