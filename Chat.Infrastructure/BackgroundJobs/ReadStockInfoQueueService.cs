using Chat.Domain.Dtos;
using Chat.Domain.Interfaces.MessageBus;
using Chat.Infrastructure.Hubs;
using Chat.Share.Events;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace Chat.Infrastructure.BackgroundJobs
{
    public class ReadStockInfoQueueService : IJob
    {
        private readonly IHubContext<SignalRHub> _hubContext;
        private IMessageBusService _messageBusService;

        public ReadStockInfoQueueService(IHubContext<SignalRHub> hubContext, IMessageBusService messageBusService)
        {
            _hubContext = hubContext;
            _messageBusService = messageBusService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _messageBusService.RecieveMessage(async (StockResponse data) =>
            {
                var chatMsg = new HubChatMessageDto(-1, "Bot", data.ChatId, data.StockInfo);

                await _hubContext.Clients.Groups(data.ChatId.ToString()).SendAsync("ReceiveMessage", chatMsg);

            }, context.CancellationToken);

        }
    }
}
