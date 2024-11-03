using ExchangeRates.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Infra.Repositories;

internal class UnitOfWork: IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(ExchangeRatesDbContext dbContext)
    {
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

    public IExchangeRepository ExchangeRepository {  get; set; }
}
