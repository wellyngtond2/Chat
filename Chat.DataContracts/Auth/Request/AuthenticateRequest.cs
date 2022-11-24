using Chat.DataContracts.Auth.Response;
using MediatR;

namespace Chat.DataContracts.Auth.Request
{
    public sealed class AuthenticateRequest : IRequest<TokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
