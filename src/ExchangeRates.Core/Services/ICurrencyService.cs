namespace ExchangeRates.Core.Services;

public interface ICurrencyService
{
    bool IsCurrencyValid(string? iso);
}
