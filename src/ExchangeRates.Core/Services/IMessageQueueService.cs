namespace ExchangeRates.Core.Services;

public interface IMessageQueueService
{
    Task SendAsync<T>(T message) where T : class;
}
