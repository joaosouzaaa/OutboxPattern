using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Publishers;
using Tickets.Microservice.Options;

namespace Tickets.Microservice.Publishers;

public sealed class OutboxPublisher(IOptions<RabbitMQCredentialsOptions> rabbitMQCredentialsOptions) : IOutboxPublisher
{
    private readonly RabbitMQCredentialsOptions _rabbitMQCredentials = rabbitMQCredentialsOptions.Value;

    public void PublishOutboxMessage(Outbox outbox)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQCredentials.HostName,
            Port = _rabbitMQCredentials.Port,
            UserName = _rabbitMQCredentials.UserName,
            Password = _rabbitMQCredentials.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: outbox.Type, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(outbox.Content);

        channel.BasicPublish(exchange: "", routingKey: outbox.Type, basicProperties: null, body: body);
    }
}
