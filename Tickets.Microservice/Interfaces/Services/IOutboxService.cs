namespace Tickets.Microservice.Interfaces.Services;

public interface IOutboxService
{
    Task AddAsync<TEntity>(TEntity entity);
}
