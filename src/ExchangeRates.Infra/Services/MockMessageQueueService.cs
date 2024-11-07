using ExchangeRates.Core.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExchangeRates.Infra.Services;

public class MockMessageQueueService(ILogger<MockMessageQueueService> logger) : IMessageQueueService
{
    private static readonly Queue<string> Queue = new Queue<string>();

    public Task SendAsync<T>(T message) where T : class
    {
        var content = JsonConvert.SerializeObject(message);

        Queue.Enqueue(content);

        logger.LogWarning($"A new item has been added to the queue : {content}. Queue content: {JsonConvert.SerializeObject(Queue)}");

        return Task.CompletedTask;
    }
}