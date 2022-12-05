using Chat.DataContracts.Auth.Response;
using Chat.DataContracts.Base;
using MediatR;

namespace Chat.DataContracts.Auth.Request
{
    public sealed class AuthenticateRequest : IRequest<BaseResponse<TokenResponse>>
    {
        public AuthenticateRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get;  }
    }
}
