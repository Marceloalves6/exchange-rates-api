using ExchangeRates.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRates.Test")]
namespace ExchangeRates.Infra.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(ExchangeRatesDbContext? dbContext)
    {
        if (dbContext is null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
        ExchangeRepository = new ExchangeRepository(dbContext);
    }
    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public IExchangeRepository ExchangeRepository { get; set; }
}
