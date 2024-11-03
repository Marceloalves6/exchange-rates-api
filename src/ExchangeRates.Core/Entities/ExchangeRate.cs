namespace ExchangeRates.Core.Entities;

public record ExchangeRate : BaseEntity
{
    public string? CurrencyFrom { get; set; }

    public string? CurrencyTo { get; set; }

    public decimal PriceBid { get; set; }

    public decimal PriceAsk { get; set; }
}

