using ExchangeRates.Core.Entities;

namespace ExchangeRates.Core.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task<TEntity?> GetExternalById(Guid externalId);
}
