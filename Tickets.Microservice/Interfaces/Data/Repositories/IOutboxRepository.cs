using Tickets.Microservice.Entities;

namespace Tickets.Microservice.Interfaces.Data.Repositories;

public interface IOutboxRepository
{
    Task<bool> AddAsync(Outbox outbox);
    Task<bool> UpdateProcessedDateAsync(Guid id, DateTime processedDate);
    Task<List<Outbox>> GetAllUnprocessedMessagesAsync();
}
