namespace GetStockBot.ExternalServices
{
    public interface IStockService
    {
        Task<byte[]> GetStockByCodeAsync(string code);
        Task<string> ProcessStockByFileAsync(byte[] file);
    }
}
