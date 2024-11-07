using MediatR;

namespace ExchangeRates.Core.Commands;

public record GetExchangeRateRequest(string? CurrencyFrom, string? CurrencyTo);
public record GetExchangeRateCommand(GetExchangeRateRequest GetExchangeRateRequest) : IRequest<GetExchangeRateResponse>;

public record GetExchangeRateResponse
{
    public Guid Id { get; init; }
    public required string CurrencyFrom { get; init; }
    public required string CurrencyTo { get; init; }
    public decimal BidPrice { get; init; }
    public decimal AskPrice { get; init; }
}