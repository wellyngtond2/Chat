namespace Chat.Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {

        }
        protected BaseEntity(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public virtual Membership Creator { get; set; }
    }
}
