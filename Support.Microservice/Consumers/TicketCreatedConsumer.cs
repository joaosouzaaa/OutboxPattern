using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Support.Microservice.Contracts;
using Support.Microservice.Interfaces.Services;
using Support.Microservice.Options;
using System.Text;
using System.Text.Json;

namespace Support.Microservice.Consumers;

public sealed class TicketCreatedConsumer(IOptions<RabbitMQCredentialsOptions> rabbitMQCredentialsOptions,
                                          IServiceScopeFactory scopeFactory)
                                          : BackgroundService
{
    private readonly RabbitMQCredentialsOptions _rabbitMQCredentials = rabbitMQCredentialsOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

        var ticketCreatedEventType = typeof(TicketCreatedEvent).ToString();

        channel.QueueDeclare(queue: ticketCreatedEventType, durable: false, exclusive: false, autoDelete: false, arguments: null);

        channel.QueueBind(ticketCreatedEventType, "", ticketCreatedEventType);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            await SendTicketCreatedEmailAsync(eventArgs);
        };

        channel.BasicConsume(queue: ticketCreatedEventType, autoAck: true, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            const int millisecondsDelay = 1000;
            await Task.Delay(millisecondsDelay, stoppingToken);
        }
    }

    private async Task SendTicketCreatedEmailAsync(BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var ticketCreatedEvent = JsonSerializer.Deserialize<TicketCreatedEvent>(message)!;

        using var scope = scopeFactory.CreateScope();

        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        await emailService.SendTicketCreatedEmailAsync(ticketCreatedEvent);
    }
}
