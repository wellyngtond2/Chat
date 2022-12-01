﻿using Chat.Infrastructure.Context;
using Chat.Infrastructure.Interceptos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Presentation.Extensions
{
    public static class DataBaseExtensions
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<FillEntitiesInterceptor>();
            services.AddSingleton<SendNotificationsInterceptor>();

            services.AddDbContext<ApiContext>((sp, op) =>
            {
                var fillEntitiesInterceptor = sp.GetService<FillEntitiesInterceptor>()!;
                var sendNotificationsInterceptor = sp.GetService<SendNotificationsInterceptor>()!;

                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(fillEntitiesInterceptor, sendNotificationsInterceptor);
            });

            return services;
        }
    }
}