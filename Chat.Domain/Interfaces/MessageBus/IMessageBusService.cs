using Chat.Share.Events.Interfaces;

namespace Chat.Domain.Interfaces.MessageBus
{
    public interface IMessageBusService
    {
        Task<R> SendMessage<T, R>(T data, CancellationToken cancellationToken = default) where T : IEvent where R : IMessage;
        Task SendMessage<T>(T data, CancellationToken cancellationToken = default) where T : IEvent;
    }
}
