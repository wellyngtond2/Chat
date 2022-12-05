using Chat.Share.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Presentation.Extensions
{
    public static class SettingsExtensions
    {
        public static IServiceCollection RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
            services.Configure<QueueSettings>(configuration.GetSection("QueueSettings"));
            services.Configure<BackgroundJobSettings>(configuration.GetSection("BackgroundJobSettings"));


            return services;
        }
    }
}
