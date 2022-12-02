using Chat.Domain.Interfaces.MessageBus;
using Chat.Share.Events.Interfaces;
using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;

namespace Chat.Infrastructure.MessageBus
{
    public class RabbitMQServiceBus : IMessageBusService
    {
        private IBus _bus;
        private readonly string ConnectionString = "host=localhost;port=5672;username=guest;password=guest";

        public async Task<R> SendMessage<T, R>(T data, CancellationToken cancellationToken)
            where T : IEvent
            where R : Share.Events.Interfaces.IMessage
        {
            _bus = RabbitHutch.CreateBus(ConnectionString);

            var response = await _bus.Rpc.RequestAsync<T, R>(data, cancellationToken);

            return response;
        }

        public async Task SendMessage<T>(T data, CancellationToken cancellationToken) where T : IEvent
        {
            _bus = RabbitHutch.CreateBus(ConnectionString);

            var message = new Message<string>(JsonConvert.SerializeObject(data));

            var exchange = new Exchange("chat.stock.direct", type: "topic");

            await _bus.Advanced.PublishAsync(exchange, "GetStockEvent", true, message: message, cancellationToken: cancellationToken);
        }
    }
}
