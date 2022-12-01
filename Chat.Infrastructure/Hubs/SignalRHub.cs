using Chat.Domain.Dtos;
using Chat.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Hubs
{
    public class SignalRHub : Hub<IHubChatService>
    {
        public async Task SendMessageToChat(HubChatMessageDto messageDto)
        {
            var x = Clients.All.ReceiveMessage(messageDto);
        }
    }
}
