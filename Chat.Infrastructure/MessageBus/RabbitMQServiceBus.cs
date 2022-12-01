using Chat.Domain.Interfaces.MessageBus;
using Chat.Share.Events.Interfaces;
using EasyNetQ;
using Newtonsoft.Json;

namespace Chat.Infrastructure.MessageBus
{
    public class RabbitMQServiceBus : IMessageBusService
    {
        private IBus _bus;

        public async Task<R> SendMessage<T, R>(T data)
            where T : IEvent
            where R : Share.Events.Interfaces.IMessage
        {
            _bus = RabbitHutch.CreateBus("host=localhost");

            var response = await _bus.Rpc.RequestAsync<T, R>(data);

            return response;
        }

        public async Task SendMessage<T>(T data) where T : IEvent
        {
            _bus = RabbitHutch.CreateBus("host=localhost");

            var message = JsonConvert.SerializeObject(data);

            var topic = data.GetType().Name;

            await _bus.PubSub.PublishAsync(message, topic);
        }
    }
}
