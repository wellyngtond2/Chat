namespace Chat.Domain.Entities
{
    public class Membership : BaseEntity
    {
        public Membership(int id, string name, string email, string password) : base(id)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public Membership()
        {

        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
