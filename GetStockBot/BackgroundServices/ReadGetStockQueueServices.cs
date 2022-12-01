using Chat.Share.Events;
using EasyNetQ;
using FluentValidation.Results;
using Quartz;

namespace GetStockBot.BackgroundServices
{
    public class ReadGetStockQueueServices : IJob
    {
        private IBus _bus;

        public ReadGetStockQueueServices(IBus bus)
        {
            _bus = bus;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _bus = RabbitHutch.CreateBus("host=localhost");

            await _bus.Rpc.RespondAsync<StockRequestedEvent, ResponseMessage>( async request =>
                new ResponseMessage(await GetStoke(request))
            );
        }

        private Task<ValidationResult> GetStoke(StockRequestedEvent request)
        {
            throw new NotImplementedException();
        }
    }
}
