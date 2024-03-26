
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Data.Repositories;
using Tickets.Microservice.Interfaces.Publishers;

namespace Tickets.Microservice.BackgroundServices;

public sealed class OutboxBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();

        var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
        var outboxPublisher = scope.ServiceProvider.GetRequiredService<IOutboxPublisher>();

        List<Outbox> outboxList = await outboxRepository.GetAllUnprocessedMessagesAsync();

        foreach (Outbox message in outboxList)
        {
            try
            {
                outboxPublisher.PublishOutboxMessage(message);
            }
            catch
            {
                continue;
            }

            await outboxRepository.UpdateProcessedDateToCurrentDateAsync(message.Id);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            const int millisecondsDelay = 1000;
            await Task.Delay(millisecondsDelay, stoppingToken);
        }
    }
}
