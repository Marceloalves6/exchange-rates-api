using MediatR;

namespace ExchangeRates.Core.Operations;

public record GetExchangeRateRequest(string CurrencyFrom, string CurrencyTo) : IRequest<GetExchangeRateResponse>;

public record GetExchangeRateResponse
{
    public Guid Id { get; init; }
    public required string CurrencyFrom { get; init; }
    public required string CurrencyTo { get; init; }
    public decimal PriceBid { get; init; }
    public decimal PriceAsk { get; init; }
}