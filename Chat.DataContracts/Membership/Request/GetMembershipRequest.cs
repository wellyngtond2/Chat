using Chat.DataContracts.Membership.Response;
using MediatR;

namespace Chat.DataContracts.Membership.Request
{
    public sealed class GetMembershipRequest : IRequest<MembershipResponse>
    {
        public GetMembershipRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
