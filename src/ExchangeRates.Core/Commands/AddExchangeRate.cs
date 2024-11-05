using MediatR;
using System.Text.Json.Serialization;

namespace ExchangeRates.Core.Commands;

public record AddExchangeRateRequest(string CurrencyFrom, string CurrencyTo, decimal BidPrice, decimal AskPrice);
public record AddExchangeRateCommand(AddExchangeRateRequest AddExchangeRateRequest) : IRequest<AddExchangeRateResponse>;
public record AddExchangeRateResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("currencyFrom ")]
    public required string CurrencyFrom { get; init; }

    [JsonPropertyName("currencyTo")]
    public required string CurrencyTo { get; init; }

    [JsonPropertyName("bidPrice")]
    public decimal BidPrice { get; init; }
    
    [JsonPropertyName("askPrice")]
    public decimal AskPrice{ get; init; }
}

