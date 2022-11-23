namespace Chat.Domain.Entities
{
    public class ChatRoom : EntityBase
    {
        public ChatRoom(string name)
        {
            Name = name;
        }

        public string Name { get;}

    }
}
