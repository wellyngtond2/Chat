using Chat.Domain.Exceptions;
using Chat.Share.Entities;

namespace Chat.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public ChatMessage()
        {

        }
        public ChatMessage(int id, int chatRoomId, string message) 
        {
            Id= id;
            Message = message;
            ChatRoomId = chatRoomId;
        }
        public int Id { get; private set; }
        public string Message { get; private set; }
        public int CreatorId { get; private set; }
        public virtual Membership Creator { get; set; }
        public int ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; set; }

        public void SetCreator(Membership user)
        {
            if (user is null)
                throw new MemberhipNullException("Invalid membership in ChatMessage");

            CreatorId = user.Id;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
