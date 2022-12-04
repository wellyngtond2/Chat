using Chat.Share.Events.Interfaces;

namespace Chat.Share.Events
{
    public class StockResponse : IntegrationEvent
    {
        public int ChatId { get; set; }
        public string StockInfo { get; set; }
        public StockResponse(int chatId, string stockInfo)
        {
            ChatId = chatId;
            StockInfo = stockInfo;
        }
    }
}
