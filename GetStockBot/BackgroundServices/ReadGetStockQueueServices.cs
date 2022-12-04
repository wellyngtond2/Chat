using Chat.Share.Events;
using EasyNetQ;
using EasyNetQ.Topology;
using GetStockBot.ExternalServices;
using Newtonsoft.Json;
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

            var message = await _bus.SendReceive.ReceiveAsync<StockRequested>("StockRequested", async (mess) =>
            {
                await GetStoke(mess.ChatId, mess.StockName, context.CancellationToken);
            }, context.CancellationToken);

        }

        private async Task GetStoke(int chatId, string request, CancellationToken cancellationToken = default)
        {
            var file = await _stockService.GetStockByCodeAsync(request);

            if (file is not null)
            {
                var stock = await _stockService.ProcessStockByFileAsync(file);

                var content = new StockResponse(chatId, stock);

                _bus = RabbitHutch.CreateBus(ConnectionString);

                await _bus.SendReceive.SendAsync(nameof(StockResponse), content, cancellationToken);
            }
        }
    }
}
