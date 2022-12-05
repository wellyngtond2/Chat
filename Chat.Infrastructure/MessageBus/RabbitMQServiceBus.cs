using Chat.Domain.Interfaces.MessageBus;
using Chat.Share.Events.Interfaces;
using Chat.Share.Settings;
using EasyNetQ;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.MessageBus
{
    public class RabbitMQServiceBus : IMessageBusService
    {
        private IBus _bus;
        private readonly QueueSettings _settings;
        public RabbitMQServiceBus(IOptions<QueueSettings> options)
        {
            _settings= options.Value;
        }

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
                _bus = RabbitHutch.CreateBus(_settings.ConnectionString);
            }
        }
    }
}
