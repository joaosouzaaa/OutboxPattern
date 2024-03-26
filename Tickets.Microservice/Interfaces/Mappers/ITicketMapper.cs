using Tickets.Microservice.DataTransferObjects.Ticket;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Interfaces.Mappers;

public interface ITicketMapper
{
    Ticket SaveToDomain(TicketSave ticketSave);
    void UpdateToDomain(TicketUpdate ticketUpdate, Ticket ticket);
    TicketResponse DomainToResponse(Ticket ticket);
    PageList<TicketResponse> DomainPageListToResponsePageList(PageList<Ticket> ticketPageList);
}
