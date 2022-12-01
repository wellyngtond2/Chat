using Chat.Share.Entities;

namespace Chat.Domain.Entities
{
    public class Membership : BaseEntity
    {
        protected Membership()
        {

        }
        public Membership(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
        public Membership(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
