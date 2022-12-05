using Chat.Share.Settings;
using GetStockBot.BackgroundServices;
using GetStockBot.ExternalServices;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection("QueueSettings"));

builder.Services.AddHttpClient(StockService.STOCK_SERVICE_NAME, client =>
{
    var stockSettings = builder.Configuration.GetSection("StockSettings").Get<StockSettings>();
    client.BaseAddress = new Uri(stockSettings.BaseUrl);
});

builder.Services.AddQuartz(config =>
{
    var jobettings = builder.Configuration.GetSection("BackgroundJobSettings").Get<BackgroundJobSettings>();
    var jobKey = new JobKey(nameof(ReadGetStockQueueServices));

    config.AddJob<ReadGetStockQueueServices>(jobKey)
            .AddTrigger(trigger =>
            trigger.ForJob(jobKey)
            .WithSimpleSchedule(schedule =>
            schedule.WithIntervalInSeconds(jobettings.BotScheduleIntervalInSeconds)
            .RepeatForever()
                ));

    config.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddScoped<IStockService, StockService>();


builder.Services.AddQuartzHostedService();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();