using Chat.Share.Events;
using EasyNetQ;
using GetStockBot.ExternalServices;
using Quartz;

namespace GetStockBot.BackgroundServices
{
    public class ReadGetStockQueueServices : IJob
    {
        private IBus _bus;
        private readonly IStockService _stockService;
        private readonly string ConnectionString = "host=localhost;port=5672;username=guest;password=guest";

        public ReadGetStockQueueServices(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _bus = RabbitHutch.CreateBus(ConnectionString);


            var message = await _bus.PubSub.SubscribeAsync<string>("StockRequested", async (mess) =>
            {
                await GetStoke(mess);
            }, 
            (x) => 
            { 
                x.WithTopic("chat.stock.direct");
                x.WithQueueName("StockRequested");
            });

        }

        private async Task GetStoke(string request)
        {
            //await _stockService.GetStockByCode(request.StockName);


        }
    }
}
