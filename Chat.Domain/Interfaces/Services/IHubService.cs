using Chat.Domain.Dtos;

namespace Chat.Domain.Interfaces.Services
{
    public interface IHubService
    {
        Task SendMessageToChatAsync(int chatId, string message);
        Task<ICollection<HubMessageDto>> GetMessagesByChatIDAsync(int chatId);
    }
}
