namespace Chat.Domain.Entities
{
    public class Membership : EntityBase
    {
        public Membership(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get;  }
        public string Email { get; }
        public string Password { get;  }
    }
}
