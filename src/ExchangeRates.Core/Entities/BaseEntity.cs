namespace ExchangeRates.Core.Entities;

public record class BaseEntity
{
    public long Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Deleted { get; set; }
}
