using Chat.Domain.Dtos;

namespace Chat.Domain.Interfaces.Services
{
    public interface IHubChatService
    {
        Task ReceiveMessage(HubChatMessageDto message);
    }
}
