using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.Membership.Request;
using FluentValidation;
using MediatR;
using Serilog;

namespace Chat.Application.Handlers.Commands.Membership
{
    public class RegisterMembershipHandler : BaseCommandHandler<RegisterMembershipRequest, Unit>
    {
        public RegisterMembershipHandler(IEnumerable<IValidator<RegisterMembershipRequest>> validators, ILogger logger, IMapper mapper) : base(validators, logger, mapper)
        {
        }

        protected override Task<Unit> Process(RegisterMembershipRequest request, CancellationToken cancellationToken)
        {
            var membership = _mapper.Map<Domain.Entities.Membership>(request);
            throw new NotImplementedException();
        }
    }
}
