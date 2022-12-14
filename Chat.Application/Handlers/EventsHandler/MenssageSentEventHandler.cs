using Chat.Domain.DomainEvents;
using Chat.Domain.Dtos;
using Chat.Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Application.Handlers.EventsHandler
{
    public sealed class MenssageSentEventHandler : INotificationHandler<MenssageSentEvent>
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public MenssageSentEventHandler(IHubContext<SignalRHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(MenssageSentEvent notification, CancellationToken cancellationToken)
        {
            var request = new HubChatMessageDto(notification.UserId, notification.User, notification.ChatId, notification.Message);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", request);
        }
    }
}
