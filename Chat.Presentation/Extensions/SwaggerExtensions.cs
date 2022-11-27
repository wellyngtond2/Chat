using Microsoft.Extensions.DependencyInjection;

namespace Chat.Presentation.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x => {
                x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http
                });
                x.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement());
            });
            return services;
        }
    }
}
