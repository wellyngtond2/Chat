using Chat.DataContracts.Auth.Response;
using Chat.DataContracts.Base;
using MediatR;

namespace Chat.DataContracts.Auth.Request
{
    public sealed class AuthenticateRequest : IRequest<BaseResponse<TokenResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
