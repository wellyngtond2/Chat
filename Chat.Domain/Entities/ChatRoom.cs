namespace Chat.Domain.Entities
{
    public class ChatRoom : BaseEntity
    {
        public ChatRoom()
        {

        }
        public ChatRoom(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void SetCreator(int creatorId)
        {
            CreatorId = creatorId;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreatorId { get; private set; }
        public virtual Membership Creator { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }

    }
}
