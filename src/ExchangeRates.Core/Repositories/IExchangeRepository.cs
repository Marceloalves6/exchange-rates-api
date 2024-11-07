using ExchangeRates.Core.Entities;

namespace ExchangeRates.Core.Repositories;

public interface IExchangeRepository : IRepository<ExchangeRate>
{
    Task<ExchangeRate?> GetAsync(string? currencyFrom, string? currencyTo);
}
