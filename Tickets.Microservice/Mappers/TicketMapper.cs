using Tickets.Microservice.DataTransferObjects.Ticket;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Mappers;
using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Mappers;

public sealed class TicketMapper : ITicketMapper
{
    public Ticket SaveToDomain(TicketSave ticketSave) =>
        new()
        {
            FirstAppearance = ticketSave.FirstAppearance,
            CreatedDate = DateTime.UtcNow,
            Description = ticketSave.Description,
            Number = ticketSave.Number,
            Tag = ticketSave.Tag,
            Title = ticketSave.Title
        };

    public void UpdateToDomain(TicketUpdate ticketUpdate, Ticket ticket)
    {
        ticket.FirstAppearance = ticketUpdate.FirstAppearance;
        ticket.Description = ticketUpdate.Description;
        ticket.Number = ticketUpdate.Number;
        ticket.Tag = ticketUpdate.Tag;
        ticket.Title = ticketUpdate.Title;
    }

    public TicketResponse DomainToResponse(Ticket ticket) =>
        new()
        {
            FirstAppearance = ticket.FirstAppearance,
            CreatedDate = ticket.CreatedDate,
            Description = ticket.Description,
            Id = ticket.Id,
            Number = ticket.Number,
            Tag = ticket.Tag,
            Title = ticket.Title
        };

    public PageList<TicketResponse> DomainPageListToResponsePageList(PageList<Ticket> ticketPageList) =>
        new()
        {
            CurrentPage = ticketPageList.CurrentPage,
            PageSize = ticketPageList.PageSize,
            Result = ticketPageList.Result.Select(DomainToResponse).ToList(),
            TotalCount = ticketPageList.TotalCount,
            TotalPages = ticketPageList.TotalPages
        };
}
