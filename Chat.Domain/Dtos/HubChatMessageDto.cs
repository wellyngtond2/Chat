namespace Chat.Domain.Dtos
{
    public record HubChatMessageDto(int UserId, string User, int ChatId, string Message);
}
