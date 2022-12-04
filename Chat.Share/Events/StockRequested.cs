using Chat.Share.Events.Interfaces;

namespace Chat.Share.Events
{
    public record StockRequested(int ChatId, string StockName) : IntegrationEvent;
}
