using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.Membership.Request;
using Chat.DataContracts.Membership.Response;
using FluentValidation;
using Serilog;

namespace Chat.Application.Handlers.Queries.Membership
{
    public class GetMembershipHandler : BaseCommandHandler<GetMembershipRequest, MembershipResponse>
    {
        public GetMembershipHandler(IEnumerable<IValidator<GetMembershipRequest>> validators, ILogger logger, IMapper mapper) : base(validators, logger, mapper)
        {
        }

        protected override Task<MembershipResponse> Process(GetMembershipRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
