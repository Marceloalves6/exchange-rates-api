namespace ExchangeRates.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    IExchangeRepository ExchangeRepository { get; set; }
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
