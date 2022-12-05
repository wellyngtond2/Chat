using CsvHelper;
using CsvHelper.Configuration;
using GetStockBot.Dtos;
using System.Globalization;

namespace GetStockBot.ExternalServices
{
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;
        public const string STOCK_SERVICE_NAME = "Stock";
        public StockService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient(STOCK_SERVICE_NAME);
        }
        public async Task<byte[]> GetStockByCodeAsync(string code)
        {
            var url = $"?s={code}&f=sd2t2ohlcv&h&e=csv";

            var httpResponse = await _httpClient.GetAsync(url);

            if (httpResponse == null || !httpResponse.IsSuccessStatusCode) return null;

            var fileResponse = await httpResponse.Content.ReadAsByteArrayAsync();

            return fileResponse;
        }

        public async Task<string> ProcessStockByFileAsync(byte[] file)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ","
            };
            try
            {
                using (var reader = new StreamReader(new MemoryStream(file)))
                using (var csv = new CsvReader(reader, configuration))
                {
                    var records = csv.GetRecords<StockDto>().ToArray();

                    return await Task.FromResult($"{records[0].Symbol} quote is ${records[0].Close} per share");
                }

            }
            catch (Exception ex)
            {
                return await Task.FromResult(string.Empty);
            }
        }
    }
}
