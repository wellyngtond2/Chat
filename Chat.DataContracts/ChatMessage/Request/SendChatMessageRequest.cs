using MediatR;

namespace Chat.DataContracts.ChatMessage.Request
{
    public sealed class SendChatMessageRequest : IRequest
    {
        public SendChatMessageRequest(int chatRoomId, string message)
        {
            ChatRoomId = chatRoomId;
            Message = message;
        }

        public string Message { get; }
        public int ChatRoomId { get; }
    }
}
