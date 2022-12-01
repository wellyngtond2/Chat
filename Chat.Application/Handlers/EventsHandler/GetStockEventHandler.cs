using Chat.Domain.DomainEvents;
using Chat.Domain.Interfaces.MessageBus;
using Chat.Share.Events;
using MediatR;

namespace Chat.Application.Handlers.EventsHandler
{
    public class GetStockEventHandler : INotificationHandler<GetStockEvent>
    {
        private readonly IMessageBusService _messageBusService;

        public GetStockEventHandler(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public async Task Handle(GetStockEvent notification, CancellationToken cancellationToken)
        {
            await _messageBusService.SendMessage(new StockRequestedEvent(notification.ChatId, notification.StockName));
        }
    }
}
