using GetStockBot.BackgroundServices;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuartz(config =>
{
    var jobKey = new JobKey(nameof(ReadGetStockQueueServices));

    config.AddJob<ReadGetStockQueueServices>(jobKey)
            .AddTrigger(trigger =>
            trigger.ForJob(jobKey)
            .WithSimpleSchedule(schedule =>
            schedule.WithIntervalInSeconds(10)
            .RepeatForever()
                )) ;

    config.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddQuartzHostedService();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();


app.Run();