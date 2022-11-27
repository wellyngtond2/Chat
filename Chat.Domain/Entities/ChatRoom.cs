namespace Chat.Domain.Entities
{
    public class ChatRoom : BaseEntity
    {
        public ChatRoom(int id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public List<ChatMessage> ChatMessages { get; set; }

    }
}
