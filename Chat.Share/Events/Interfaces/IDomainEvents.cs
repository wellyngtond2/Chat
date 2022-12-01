using MediatR;

namespace Chat.Share.Events.Interfaces
{
    public interface IDomainEvents : IEvent, INotification { }
}
