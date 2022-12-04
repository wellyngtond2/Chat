using Chat.Domain.Exceptions;
using Chat.Share.Entities;

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

        public void SetCreator(Membership user)
        {
            if (user is null)
                throw new MemberhipNullException("Invalid membership in ChatRoom");

            CreatorId = user.Id;
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreatorId { get; private set; }
        public virtual Membership Creator { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }

    }
}
