using Chat.Application;
using Chat.Infrastructure.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Configuration;

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

            return services;
        }
    }
}
