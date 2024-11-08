using ExchangeRates.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Infra;

public class ExchangeRatesDbContext(DbContextOptions<ExchangeRatesDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExchangeRate>().HasKey(x => x.Id);

        modelBuilder.Entity<ExchangeRate>().Property(x => x.ExternalId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<ExchangeRate>().Property(x => x.CurrencyFrom)
            .HasMaxLength(15);
        modelBuilder.Entity<ExchangeRate>().Property(x => x.CurrencyTo)
            .HasMaxLength(15);
    }

    public DbSet<ExchangeRate> ExchangeRate { get; set; }
}
