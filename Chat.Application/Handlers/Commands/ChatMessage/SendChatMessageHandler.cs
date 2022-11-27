using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.ChatMessage.Request;
using Chat.Infrastructure.Context;
using FluentValidation;
using MediatR;
using Serilog;

namespace Chat.Application.Handlers.Commands.ChatMessage
{
    internal class SendChatMessageHandler : BaseCommandHandler<SendChatMessageRequest, Unit>
    {
        private readonly ApiContext _dbContext;
        public SendChatMessageHandler(IEnumerable<IValidator<SendChatMessageRequest>> validators, ILogger logger, IMapper mapper, ApiContext dbContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
        }

        protected async override Task<Unit> Process(SendChatMessageRequest request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<Domain.Entities.ChatMessage>(request);

            _dbContext.Add(message);

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return default;
        }
    }
}
