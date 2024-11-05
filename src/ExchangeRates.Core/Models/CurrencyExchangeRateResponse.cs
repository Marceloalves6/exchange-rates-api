using System.Text.Json.Serialization;

namespace ExchangeRates.Core.Models;

public class RealtimeCurrencyExchangeRate 
{
    [JsonPropertyName("1. From_Currency Code")] 
    public string? FromCurrencyCode { get; init; }
    
    [JsonPropertyName("2. From_Currency Name")] 
    public string? FromCurrencyName { get; init; }
    
    [JsonPropertyName("3. To_Currency Code")] 
    public string? ToCurrencyCode { get; init; }
    
    [JsonPropertyName("4. To_Currency Name")]
    public string? ToCurrencyName { get; init; }
    
    [JsonPropertyName("5. Exchange Rate")] 
    public string? ExchangeRate { get; init; }
    
    [JsonPropertyName("6. Last Refreshed")] 
    public string? LastRefreshed { get; init; }
    
    [JsonPropertyName("7. Time Zone")]
    public string? TimeZone { get; init; }
    
    [JsonPropertyName("8. Bid Price")]
    public decimal BidPrice { get; init; }
    
    [JsonPropertyName("9. Ask Price")]
    public decimal AskPrice { get; init; }
}

public class CurrencyExchangeRateResponse
{
    [JsonPropertyName("Realtime Currency Exchange Rate")]
    public  RealtimeCurrencyExchangeRate? RealtimeCurrencyExchangeRate { get; set; }
}

/*
{
"Realtime Currency Exchange Rate": {
    "1. From_Currency Code": "USD",
    "2. From_Currency Name": "United States Dollar",
    "3. To_Currency Code": "EUR",
    "4. To_Currency Name": "Euro",
    "5. Exchange Rate": "0.91720000",
    "6. Last Refreshed": "2024-11-05 12:13:01",
    "7. Time Zone": "UTC",
    "8. Bid Price": "0.91719000",
    "9. Ask Price": "0.91723000"
}
}

*/