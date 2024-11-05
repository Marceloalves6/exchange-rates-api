
using ExchangeRates.Core.Models;
using Refit;

namespace ExchangeRates.Core.Services;

public interface IAlphavantageService
{
    [Get("/query")]
    Task<CurrencyExchangeRateResponse> CurrencyExchangeRateAsync([Query("function")] string? function, 
                                      [Query("from_currency")] string? currencyFrom, 
                                      [Query("to_currency")] string? currencyTo, 
                                      [Query("apiKey")] string? apiKey);
}
