namespace Support.Microservice.Contracts;

public sealed record TicketCreatedEvent (Guid Id,
                                         int Number);
