using Tickets.Microservice.Arguments;
using Tickets.Microservice.DataTransferObjects.Ticket;
using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Interfaces.Services;

public interface ITicketService
{
    Task<bool> AddAsync(TicketSave ticketSave);
    Task<bool> UpdateAsync(TicketUpdate ticketUpdate);
    Task<bool> DeleteAsync(Guid id);
    Task<TicketResponse?> GetByIdAsync(Guid id);
    Task<PageList<TicketResponse>> GetAllPaginatedAsync(GetAllTicketsFilteredArgument filter);
}
