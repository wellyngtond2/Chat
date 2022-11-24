using Chat.DataContracts.ChatMessage.Response;
using MediatR;

namespace Chat.DataContracts.ChatMessage.Request
{
    public sealed class GetChatMessagesRequest : IRequest<ICollection<ChatMessageResponse>>
    {
        public GetChatMessagesRequest(int chatId)
        {
            ChatId = chatId;
        }

        public int ChatId { get; }
    }
}
