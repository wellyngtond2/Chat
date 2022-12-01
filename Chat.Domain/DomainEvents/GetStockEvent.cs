using Chat.Share.Events.Interfaces;

namespace Chat.Domain.DomainEvents
{
    public class GetStockEvent : IDomainEvents
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
