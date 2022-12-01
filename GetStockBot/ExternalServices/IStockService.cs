namespace GetStockBot.ExternalServices
{
    public interface IStockService
    {
        Task GetStockByCode(string code);
    }
}
