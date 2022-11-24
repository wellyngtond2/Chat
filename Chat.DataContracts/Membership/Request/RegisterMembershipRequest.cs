using MediatR;

namespace Chat.DataContracts.Membership.Request
{
    public sealed class RegisterMembershipRequest : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
