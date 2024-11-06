
using ExchangeRates.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;


namespace ExchangeRates.Infra.Services;

public class MessageQueueService(ILogger<MessageQueueService> logger, IConfiguration configuration) : IMessageQueueService
{
    private readonly ILogger<MessageQueueService> _logger;

    public async Task SendAsync<T>(T message) where T : class
    {

        var factory = new ConnectionFactory()
        {
            HostName = configuration.GetValue<string>("RabbitMQConfiguration:Host"),
            Port = configuration.GetValue<int>("RabbitMQConfiguration:Port")
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        string? exchangeName = configuration.GetValue<string?>("RabbitMQConfiguration:Exchange");
        string? queueName = configuration.GetValue<string?>("RabbitMQConfiguration:Queue");
        string? routingKey = configuration.GetValue<string?>("RabbitMQConfiguration:RoutingKey");

        // Declare the exchange
        channel.ExchangeDeclare(exchange: exchangeName, type: "topic");

        // Declare the queue
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        // Bind the queue to the exchange with the routing key
        channel.QueueBind(queue: queueName,
                          exchange: exchangeName,
                          routingKey: routingKey);

        if (message is null) return;

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        channel.BasicPublish(exchange: exchangeName,
                             routingKey: routingKey,
                             basicProperties: null,
                             body: body);

        logger.LogInformation($"Message sent. Exchange: {exchangeName}, Queue: {queueName}", message);

        await Task.CompletedTask;
    }

}
