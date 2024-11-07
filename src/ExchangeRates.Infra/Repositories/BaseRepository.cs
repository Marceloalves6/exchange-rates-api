using ExchangeRates.Core.Entities;
using ExchangeRates.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Infra.Repositories;

internal class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _dbContext;

    public BaseRepository(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        _dbContext = dbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public Task<TEntity?> GetExternalById(Guid externalId)
    {
        return _dbContext.Set<TEntity>().FirstOrDefaultAsync(i => i.ExternalId == externalId);
    }

    public Task UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);

        return Task.CompletedTask;
    }
}
