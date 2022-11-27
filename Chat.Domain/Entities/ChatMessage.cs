namespace Chat.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public ChatMessage(int id, int chatRoomId, string message) : base(id)
        {
            Message = message;
            ChatRoomId = chatRoomId;
        }

        public string Message { get; private set; }
        public int ChatRoomId { get; private set; }
    }
}
