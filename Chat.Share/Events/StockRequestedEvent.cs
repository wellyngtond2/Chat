using Chat.Share.Events.Interfaces;

namespace Chat.Share.Events
{
    public class StockRequestedEvent : IntegrationEvent
    {
        public StockRequestedEvent(int chatId, string stockName)
        {
            ChatId = chatId;
            StockName = stockName;
        }

        public int ChatId { get; }
        public string StockName { get; }
    }
}
