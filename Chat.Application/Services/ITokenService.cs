using Chat.DataContracts.Auth.Response;
using Chat.Domain.Entities;

namespace Chat.Application.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetUserTokenAsync(Membership membership);
    }
}
