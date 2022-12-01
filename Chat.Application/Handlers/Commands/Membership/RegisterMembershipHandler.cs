using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.Application.Handlers.Events.EventsRequest;
using Chat.DataContracts.Membership.Request;
using Chat.Domain.Dtos;
using Chat.Infrastructure.Context;
using FluentValidation;
using MediatR;
using Serilog;

namespace Chat.Application.Handlers.Commands.Membership
{
    public class RegisterMembershipHandler : BaseCommandHandler<RegisterMembershipRequest, Unit>
    {
        private readonly ApiContext _dbContext;
        public RegisterMembershipHandler(IEnumerable<IValidator<RegisterMembershipRequest>> validators, ILogger logger, IMapper mapper, ApiContext dbContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
        }

        protected override async Task<Unit> Process(RegisterMembershipRequest request, CancellationToken cancellationToken)
        {
            var membership = _mapper.Map<Domain.Entities.Membership>(request);

            _dbContext.Add(membership);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
