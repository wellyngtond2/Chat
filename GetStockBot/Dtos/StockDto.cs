using CsvHelper.Configuration.Attributes;

namespace GetStockBot.Dtos;

public record StockDto([Index(0)] string Symbol, [Index(1)] string Date, [Index(2)] string Time, [Index(3)] string Open, [Index(4)] string High, [Index(5)] string Low, [Index(6)] string Close, [Index(7)] string Volume);

