using GetStockBot.BackgroundServices;
using GetStockBot.ExternalServices;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuartz(config =>
{
    var jobKey = new JobKey(nameof(ReadGetStockQueueServices));

    config.AddJob<ReadGetStockQueueServices>(jobKey)
            .AddTrigger(trigger =>
            trigger.ForJob(jobKey)
            .WithSimpleSchedule(schedule =>
            schedule.WithIntervalInSeconds(1000)
            .RepeatForever()
                ));

    config.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddQuartzHostedService();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();


app.Run();