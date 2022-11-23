namespace Chat.Domain.Entities
{
    public abstract class EntityBase 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}
