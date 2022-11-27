using Chat.Application.Services;
using Chat.Domain.Interfaces.Repository;
using Chat.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Presentation.Extensions
{
    public static class DomainServicesExtensions
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
