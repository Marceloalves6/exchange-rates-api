using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExchangeRates.Infra.Repositories;

internal class ExchangeRepository : BaseRepository<ExchangeRate>, IExchangeRepository
{
    private readonly DbContext _dbContext;
    public ExchangeRepository(DbContext? dbContext) : base(dbContext)
    {
        if (dbContext is null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
    }
    public Task<ExchangeRate?> GetAsync(string? currencyFrom, string? currencyTo)
    {
        var exchangeRate = _dbContext?.Set<ExchangeRate>().FirstOrDefault(i => i.CurrencyFrom == currencyFrom && i.CurrencyTo == currencyTo);

        return Task.FromResult(exchangeRate);
    }
}
