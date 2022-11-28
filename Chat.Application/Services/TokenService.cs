using Chat.DataContracts.Auth.Response;
using Chat.DataContracts.Settings;
using Chat.Domain.Entities;
using Chat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly Infrastructure.Context.ApiContext _dbContext;
        private readonly AuthSettings _settings;

        public TokenService(Infrastructure.Context.ApiContext dbContext, IOptions<AuthSettings> options)
        {
            _dbContext = dbContext;
            _settings = options.Value;
        }

        public async Task<TokenResponse> GetUserTokenAsync(Membership membership)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var expirationDate = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);
            var key = Encoding.UTF8.GetBytes(_settings.SecretKey);

            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", membership.Id.ToString()),
                    new Claim(ClaimTypes.Email, membership.Email),
                    new Claim(ClaimTypes.Name, membership.Name),
                })

            });

            return new TokenResponse()
            {
                ExpirationIn = (int)expirationDate.Subtract(DateTime.UtcNow).TotalMinutes,
                Type = "Bearer",
                Token = tokenHandler.WriteToken(securityToken)
            };
        }
    }
}
