using Chat.Domain.Interfaces.MessageBus;
using Chat.Infrastructure.MessageBus;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Presentation.Extensions
{
    public static class ExternalServicesExtensions
    {
        public static IServiceCollection RegisterExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageBusService, RabbitMQServiceBus>();

            return services;
        }
    }
}
