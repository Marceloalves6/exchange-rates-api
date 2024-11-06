using MediatR;
using Newtonsoft.Json;

namespace ExchangeRates.Core.Commands;


public record UpdateExchangeRateResquest(Guid Id, string CurrencyFrom, string CurrencyTo, decimal BidPrice, decimal AskPrice);
public record UpdateExchangeRateCommand(UpdateExchangeRateResquest UpdateExchangeRateResquest) : IRequest<UpdateExchangeRateResponse>;
public record UpdateExchangeRateResponse
{
    [JsonProperty("id")]
    public Guid Id { get; init; }

    [JsonProperty("currencyFrom")]
    public required string CurrencyFrom { get; init; }

    [JsonProperty("currencyTo")]

    public required string CurrencyTo { get; init; }

    [JsonProperty("bidPrice")]
    public decimal BidPrice { get; init; }

    [JsonProperty("askPrice")]
    public decimal AskPrice { get; init; }
}