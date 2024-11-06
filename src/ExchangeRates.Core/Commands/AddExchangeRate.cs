using MediatR;
using Newtonsoft.Json;


namespace ExchangeRates.Core.Commands;

public record AddExchangeRateRequest(string CurrencyFrom, string CurrencyTo, decimal BidPrice, decimal AskPrice);
public record AddExchangeRateCommand(AddExchangeRateRequest AddExchangeRateRequest) : IRequest<AddExchangeRateResponse>;
public record AddExchangeRateResponse
{
    [JsonProperty("id")]
    public Guid Id { get; init; }

    [JsonProperty("currencyFrom ")]
    public required string CurrencyFrom { get; init; }

    [JsonProperty("currencyTo")]
    public required string CurrencyTo { get; init; }

    [JsonProperty("bidPrice")]
    public decimal BidPrice { get; init; }

    [JsonProperty("askPrice")]
    public decimal AskPrice { get; init; }
}

