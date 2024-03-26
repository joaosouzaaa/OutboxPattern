using System.Text.Json;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Data.Repositories;
using Tickets.Microservice.Interfaces.Services;

namespace Tickets.Microservice.Services;

public sealed class OutboxService (IOutboxRepository outboxRepository) : IOutboxService
{
    public Task AddAsync<TEntity>(TEntity entity)
    {
        var outbox = new Outbox()
        {
            Content = JsonSerializer.Serialize(entity),
            CreatedDate = DateTime.UtcNow,
            Type = entity!.GetType().ToString()
        };

        return outboxRepository.AddAsync(outbox);
    }
}
