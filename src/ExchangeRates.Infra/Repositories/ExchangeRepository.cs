using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Repositories;
using Microsoft.EntityFrameworkCore;

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
    public Task<ExchangeRate?> GetAsync(string? currencyFrom, string? currencyTo, bool includeDeleted = false)
    {
        var exchangeRate = _dbContext?.Set<ExchangeRate>().FirstOrDefault(i => i.CurrencyFrom == currencyFrom && i.CurrencyTo == currencyTo && i.Deleted == includeDeleted);

        return Task.FromResult(exchangeRate);
    }
}
