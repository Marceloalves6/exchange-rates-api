using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Infra.Repositories;

internal class ExchangeRepository(DbContext dbContext) : BaseRepository<ExchangeRate>(dbContext), IExchangeRepository
{
    public Task<ExchangeRate?> GetAsync(string currencyFrom, string currencyTo)
    {
        var exchangeRate = dbContext.Set<ExchangeRate>().FirstOrDefault(i => i.CurrencyFrom == currencyFrom && i.CurrencyTo == currencyTo);

        return Task.FromResult(exchangeRate);
    }
}
