using FluentValidation;
using Tickets.Microservice.Arguments;
using Tickets.Microservice.Contracts;
using Tickets.Microservice.DataTransferObjects.Ticket;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Enums;
using Tickets.Microservice.Extensions;
using Tickets.Microservice.Interfaces.Data.Repositories;
using Tickets.Microservice.Interfaces.Mappers;
using Tickets.Microservice.Interfaces.Services;
using Tickets.Microservice.Interfaces.Setttings;
using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Services;

public sealed class TicketService(ITicketRepository ticketRepository, 
                                  ITicketMapper ticketMapper, 
                                  INotificationHandler notificationHandler, 
                                  IValidator<Ticket> validator,
                                  IOutboxService outboxService) 
                                  : ITicketService
{
    public async Task<bool> AddAsync(TicketSave ticketSave)
    {
        var ticket = ticketMapper.SaveToDomain(ticketSave);

        if(!await ValidateAsync(ticket))
            return false;

        var addResult = await ticketRepository.AddAsync(ticket);

        await outboxService.AddAsync(new TicketCreatedEvent(ticket.Id));

        return addResult;
    }

    public async Task<bool> UpdateAsync(TicketUpdate ticketUpdate)
    {
        var ticket = await ticketRepository.GetByIdAsync(ticketUpdate.Id, false);

        if(ticket is null)
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Ticket"));

            return false;
        }

        ticketMapper.UpdateToDomain(ticketUpdate, ticket);

        if (!await ValidateAsync(ticket))
            return false;

        return await ticketRepository.UpdateAsync(ticket);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (!await ticketRepository.ExistsAsync(id))
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Ticket"));

            return false;
        }

        return await ticketRepository.DeleteAsync(id);
    }

    public async Task<TicketResponse?> GetByIdAsync(Guid id)
    {
        var ticket = await ticketRepository.GetByIdAsync(id, true);

        if (ticket is null)
            return null;

        return ticketMapper.DomainToResponse(ticket);
    }

    public async Task<PageList<TicketResponse>> GetAllPaginatedAsync(GetAllTicketsFilteredArgument filter)
    {
        var ticketPageList = await ticketRepository.GetAllPaginatedAsync(filter);

        return ticketMapper.DomainPageListToResponsePageList(ticketPageList);
    }

    private async Task<bool> ValidateAsync(Ticket ticket)
    {
        var validationResult = await validator.ValidateAsync(ticket);

        if (validationResult.IsValid)
            return true;

        foreach(var error in validationResult.Errors)
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);

        return false;
    }
}
