using MediatR;

namespace Chat.Application.Handlers.Events.EventsRequest
{
    public class GetStockEvent : INotification
    {
        public GetStockEvent(int chatId, string stockName)
        {
            ChatId = chatId;
            StockName = stockName;
        }

        public int ChatId { get; }
        public string StockName { get; }
    }
}
