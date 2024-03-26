using Tickets.Microservice.Arguments;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Interfaces.Data.Repositories;

public interface ITicketRepository
{
    Task<bool> AddAsync(Ticket ticket);
    Task<bool> UpdateAsync(Ticket ticket);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<Ticket?> GetByIdAsync(Guid id, bool asNoTracking);
    Task<PageList<Ticket>> GetAllPaginatedAsync(GetAllTicketsFilteredArgument filter);
}
