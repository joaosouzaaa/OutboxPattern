using Support.Microservice.Contracts;

namespace Support.Microservice.Interfaces.Services;

public interface IEmailService
{
    Task SendTicketCreatedEmailAsync(TicketCreatedEvent ticketCreated, string[] toEmailList);
}
