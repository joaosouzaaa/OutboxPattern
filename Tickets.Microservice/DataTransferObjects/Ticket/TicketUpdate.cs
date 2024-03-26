namespace Tickets.Microservice.DataTransferObjects.Ticket;

public sealed record TicketUpdate(Guid Id,
                                  string Title,
                                  int Number,
                                  string Tag,
                                  string Description,
                                  DateTime FirstAppearance);
