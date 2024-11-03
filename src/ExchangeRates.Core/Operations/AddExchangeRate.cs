using MediatR;

namespace ExchangeRates.Core.Operations;

public record AddExchangeRateResquest(string CurrencyFrom, string CurrencyTo, decimal PriceBid, decimal PriceAsk) : IRequest<AddExchangeRateResponse>;
public record AddExchangeRateResponse
{
    public Guid Id { get; init; }
    public required string CurrencyFrom { get; init; }
    public required string CurrencyTo { get; init; }
    public decimal PriceBid { get; init; }
    public decimal PriceAsk { get; init; }
}