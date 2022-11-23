namespace Chat.Domain.Entities
{
    public class ChatMessage : EntityBase
    {
        public ChatMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
