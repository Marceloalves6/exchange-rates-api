using MediatR;
using System.Text.Json.Serialization;

namespace ExchangeRates.Core.Operations;

public record UpdateExchangeRateResquest(Guid Id, string CurrencyFrom, string CurrencyTo, decimal PriceBid, decimal PriceAsk) : IRequest<UpdateExchangeRateResponse>;
public record UpdateExchangeRateResponse
{
    public Guid Id { get; init; }
    public required string CurrencyFrom { get; init; }
    public required string CurrencyTo { get; init; }
    public decimal PriceBid { get; init; }
    public decimal PriceAsk { get; init; }
}