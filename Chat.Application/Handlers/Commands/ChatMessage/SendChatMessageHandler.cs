using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.Application.Handlers.Events.EventsRequest;
using Chat.Application.Services;
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
        private readonly IUserContext _userContext;
        public SendChatMessageHandler(IEnumerable<IValidator<SendChatMessageRequest>> validators, ILogger logger, IMapper mapper, ApiContext dbContext, IUserContext userContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        protected async override Task<Unit> Process(SendChatMessageRequest request, CancellationToken cancellationToken)
        {
            if (request.Message.StartsWith("/stock="))
            {
                var index = request.Message.IndexOf("=");

                var stockName = request.Message.Substring(index);

                var menssageSentEvent = new GetStockEvent(request.ChatId, stockName);

                _dbContext.Notifications.Add(menssageSentEvent);

                return default;
            }

            var userData = _userContext.GetUserContext();

            var message = _mapper.Map<Domain.Entities.ChatMessage>(request);
            var creator = new Domain.Entities.Membership(userData.userId);
            message.SetCreator(creator);
            _dbContext.Add(message);

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                var menssageSentEvent = new MenssageSentEvent(userData.userId, userData.userName, request.ChatId, request.Message);

                _dbContext.Notifications.Add(menssageSentEvent);
            }


            return default;
        }
    }
}
