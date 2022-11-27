using MediatR;

namespace Chat.DataContracts.ChatMessage.Request
{
    public sealed class SendChatMessageRequest : IRequest
    {
        public SendChatMessageRequest(int chatId, string message)
        {
            ChatId = chatId;
            Message = message;
        }

        public string Message { get; }
        public int ChatId { get; }
    }
}
