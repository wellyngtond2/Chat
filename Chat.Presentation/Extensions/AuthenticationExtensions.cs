using Chat.DataContracts.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chat.Presentation.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection RegisterAuthenticate(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("AuthSettings").Get<AuthSettings>();

            var key = Encoding.UTF8.GetBytes(settings.SecretKey);

            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true

                };
            } );

            return services;
        }
    }
}
