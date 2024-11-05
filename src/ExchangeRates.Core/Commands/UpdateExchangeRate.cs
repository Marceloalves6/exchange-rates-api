using MediatR;
using System.Text.Json.Serialization;

namespace ExchangeRates.Core.Commands;


public record UpdateExchangeRateResquest(Guid Id, string CurrencyFrom, string CurrencyTo, decimal BidPrice, decimal AskPrice);
public record UpdateExchangeRateCommand(UpdateExchangeRateResquest UpdateExchangeRateResquest) : IRequest<UpdateExchangeRateResponse>;
public record UpdateExchangeRateResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("currencyFrom")]
    public required string CurrencyFrom { get; init; }

    [JsonPropertyName("currencyTo")]

    public required string CurrencyTo { get; init; }
   
    [JsonPropertyName("bidPrice")]
    public decimal BidPrice { get; init; }

    [JsonPropertyName("askPrice")]
    public decimal AskPrice { get; init; }
}