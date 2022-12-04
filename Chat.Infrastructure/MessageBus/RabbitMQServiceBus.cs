using Chat.Domain.Interfaces.MessageBus;
using Chat.Share.Events;
using Chat.Share.Events.Interfaces;
using EasyNetQ;

namespace Chat.Infrastructure.MessageBus
{
    public class RabbitMQServiceBus : IMessageBusService
    {
        private IBus _bus;
        private readonly string ConnectionString = "host=localhost;port=5672;username=guest;password=guest";

        public async Task SendMessage<T>(T data, CancellationToken cancellationToken) where T : IEvent
        {
            TryConnect();

            var queueName = data.GetType().Name;

            await _bus.SendReceive.SendAsync(queueName, data, cancellationToken);
        }

        public async Task RecieveMessage<T>(Func<T, Task> func, CancellationToken cancellationToken = default) where T : IEvent
        {
            TryConnect();

            var queueName = typeof(T).Name;

            await _bus.SendReceive.ReceiveAsync<T>(queueName, func, cancellationToken);
        }


        private void TryConnect()
        {
            if (_bus == null || !_bus.Advanced.IsConnected)
            {
                _bus = RabbitHutch.CreateBus(ConnectionString);
            }
        }
    }
}
