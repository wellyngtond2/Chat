using Chat.Share.Events.Interfaces;

namespace Chat.Share.Entities
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvents> _domainEvents = new();
        public DateTime CreatedAt { get; set; }

        public IReadOnlyCollection<IDomainEvents> GetDomainEvents() => _domainEvents.ToList();
        public void ClearDomainEvents()=> _domainEvents.Clear();
        public void RaiseDoaminEvents(IDomainEvents domainEvents)
        {
            _domainEvents.Add(domainEvents);
        }
    }
}
