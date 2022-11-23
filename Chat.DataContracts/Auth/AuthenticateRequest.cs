using MediatR;

namespace Chat.DataContracts.Auth
{
    public class AuthenticateRequest : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
