using Chat.Domain.Exceptions;
using Chat.Share.Entities;

namespace Chat.Domain.Entities
{
    public class Membership : BaseEntity
    {
        public Membership()
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
        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordNullException("Membership password is null");

            Password = password;
        }
    }
}
