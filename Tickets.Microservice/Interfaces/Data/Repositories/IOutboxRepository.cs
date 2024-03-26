using Tickets.Microservice.Entities;

namespace Tickets.Microservice.Interfaces.Data.Repositories;

public interface IOutboxRepository
{
    Task<bool> AddAsync(Outbox outbox);
    Task<bool> UpdateProcessedDateToCurrentDateAsync(Guid id);
    Task<List<Outbox>> GetAllUnprocessedMessagesAsync();
}
