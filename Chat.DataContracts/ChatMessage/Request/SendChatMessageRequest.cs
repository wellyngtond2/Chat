using MediatR;

namespace Chat.DataContracts.ChatMessage.Request
{
    public sealed class SendChatMessageRequest : IRequest
    {
        public SendChatMessageRequest(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
