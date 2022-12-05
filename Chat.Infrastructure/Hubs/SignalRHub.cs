using Chat.Domain.Dtos;
using Chat.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Hubs
{
    public class SignalRHub : Hub<IHubChatService>
    {
        public async Task SendMessageToChat(HubChatMessageDto messageDto)
        {
            await Clients.All.ReceiveMessage(messageDto);
        }
    }
}
