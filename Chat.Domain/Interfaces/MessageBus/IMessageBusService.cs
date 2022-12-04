using Chat.Share.Events.Interfaces;

namespace Chat.Domain.Interfaces.MessageBus
{
    public interface IMessageBusService
    {
        Task SendMessage<T>(T data, CancellationToken cancellationToken = default) where T : IEvent;
        Task RecieveMessage<T>(Func<T, Task> func, CancellationToken cancellationToken = default) where T : IEvent;
    }
}
