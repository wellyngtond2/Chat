using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.Application.Services;
using Chat.DataContracts.ChatMessage.Request;
using Chat.Domain.DomainEvents;
using Chat.Domain.Exceptions;
using Chat.Infrastructure.Context;
using FluentValidation;
using MediatR;
using Serilog;

namespace Chat.Application.Handlers.Commands.ChatMessage
{
    public class SendChatMessageHandler : BaseCommandHandler<SendChatMessageRequest, Unit>
    {
        private readonly IApiContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IMediator _mediator;
        public SendChatMessageHandler(IEnumerable<IValidator<SendChatMessageRequest>> validators, ILogger logger, IMapper mapper, IApiContext dbContext, IUserContext userContext, IMediator mediator) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _mediator = mediator;
        }

        protected async override Task<Unit> Process(SendChatMessageRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Message)) return default;

            if (request.Message.StartsWith("/stock="))
            {
                var index = request.Message.IndexOf("=");

                var stockName = request.Message.Substring(index + 1);

                if (string.IsNullOrWhiteSpace(stockName)) throw new InvalidStockCodeException("Invalid stock code");

                var getStockEvent = new GetStockEvent(request.ChatRoomId, stockName);

                await _mediator.Publish(getStockEvent);

                return default;
            }

            var userData = _userContext.GetUserContext();

            var message = _mapper.Map<Domain.Entities.ChatMessage>(request);
            var creator = new Domain.Entities.Membership(userData.userId);
            message.SetCreator(creator);
            _dbContext.ChatMessages.Add(message);

            var menssageSentEvent = new MenssageSentEvent(userData.userId, userData.userName, request.ChatRoomId, request.Message);

            message.RaiseDoaminEvents(menssageSentEvent);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
