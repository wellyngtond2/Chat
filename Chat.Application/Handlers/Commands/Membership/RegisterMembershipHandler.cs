using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.Membership.Request;
using Chat.Infrastructure.Context;
using Chat.Share.Helpers;
using FluentValidation;
using MediatR;
using Serilog;

namespace Chat.Application.Handlers.Commands.Membership
{
    public class RegisterMembershipHandler : BaseCommandHandler<RegisterMembershipRequest, Unit>
    {
        private readonly IApiContext _dbContext;
        public RegisterMembershipHandler(IEnumerable<IValidator<RegisterMembershipRequest>> validators, ILogger logger, IMapper mapper, IApiContext dbContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
        }

        protected override async Task<Unit> Process(RegisterMembershipRequest request, CancellationToken cancellationToken)
        {
            var membership = _mapper.Map<Domain.Entities.Membership>(request);

            membership.SetPassword(SecurityHelper.StringToHash(request.Password));

            _dbContext.Memberships.Add(membership);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
