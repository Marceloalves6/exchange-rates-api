
using ExchangeRates.Core.Models;
using Refit;

namespace ExchangeRates.Core.Services;

public interface IAlphavantageService
{
    [Get("/query")]
    Task<CurrencyExchangeRateResponse> CurrencyExchangeRateAsync([Query] CurrencyExchangeRateParams @params);
}


public class CurrencyExchangeRateParams
{
    [AliasAs("function")]
    public string? Function { get; set; }

    [AliasAs("from_currency")]
    public string? CurrencyFrom { get; set; }

    [AliasAs("to_currency")]
    public string? CurrencyTo { get; set; }

    [AliasAs("apikey")]
    public string? ApiKey { get; set; }
}