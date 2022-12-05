using Chat.Share.Events;
using Chat.Share.Settings;
using EasyNetQ;
using GetStockBot.ExternalServices;
using Microsoft.Extensions.Options;
using Quartz;

namespace GetStockBot.BackgroundServices
{
    public class ReadGetStockQueueServices : IJob
    {
        private IBus _bus;
        private readonly IStockService _stockService;
        private readonly QueueSettings _settings;

        public ReadGetStockQueueServices(IStockService stockService, IOptions<QueueSettings> options)
        {
            _stockService = stockService;
            _settings = options.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            TryConnect();

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

                TryConnect();

                await _bus.SendReceive.SendAsync(nameof(StockResponse), content, cancellationToken);
            }
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
