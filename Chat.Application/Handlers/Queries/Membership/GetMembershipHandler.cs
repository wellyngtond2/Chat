using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.Membership.Request;
using Chat.DataContracts.Membership.Response;
using Chat.Infrastructure.Context;
using FluentValidation;
using Serilog;

namespace Chat.Application.Handlers.Queries.Membership
{
    public class GetMembershipHandler : BaseCommandHandler<GetMembershipRequest, MembershipResponse>
    {
        private readonly ApiContext _dbContext;
        public GetMembershipHandler(IEnumerable<IValidator<GetMembershipRequest>> validators, ILogger logger, IMapper mapper, ApiContext dbContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
        }

        protected override async Task<MembershipResponse> Process(GetMembershipRequest request, CancellationToken cancellationToken)
        {
            var membership = await _dbContext.Memberships.FindAsync(new { request.Id }, cancellationToken);

            if (membership is null) return default;

            var response = _mapper.Map<MembershipResponse>(membership);

            return response;
        }
    }
}
