using System.Text.Json;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Data.Repositories;
using Tickets.Microservice.Interfaces.Services;

namespace Tickets.Microservice.Services;

public sealed class OutboxService (IOutboxRepository outboxRepository) : IOutboxService
{
    public Task AddAsync<TEntity>(TEntity entity)
    {
        var type = entity!.GetType().ToString();

        var outbox = new Outbox()
        {
            Content = JsonSerializer.Serialize(entity),
            CreatedDate = DateTime.UtcNow,
            Type = type.Substring(type.LastIndexOf('.') + 1)
        };

        return outboxRepository.AddAsync(outbox);
    }
}
