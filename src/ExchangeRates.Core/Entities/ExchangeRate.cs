namespace ExchangeRates.Core.Entities;

public record ExchangeRate : BaseEntity
{
    public string? CurrencyFrom { get; set; }

    public string? CurrencyTo { get; set; }

    public decimal BidPrice { get; set; }

    public decimal AskPrice { get; set; }
}

