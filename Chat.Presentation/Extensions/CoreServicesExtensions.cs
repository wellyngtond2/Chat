using Chat.Infrastructure.BackgroundJobs;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Serilog;

namespace Chat.Presentation.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            services.AddMediatR(typeof(Chat.Application.AssemblyReference).Assembly);
            services.AddAutoMapper(typeof(Chat.Application.AssemblyReference).Assembly);
            services.AddValidatorsFromAssembly(typeof(Chat.Application.AssemblyReference).Assembly);

            services.AddQuartz(config =>
            {
                var jobKey = new JobKey(nameof(ReadStockInfoQueueService));

                config.AddJob<ReadStockInfoQueueService>(jobKey)
                        .AddTrigger(trigger =>
                        trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(15)
                        .RepeatForever()));

                config.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();

            return services;
        }
    }
}
