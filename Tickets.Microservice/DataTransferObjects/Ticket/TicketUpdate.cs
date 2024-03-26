namespace Tickets.Microservice.DataTransferObjects.Ticket;

public sealed record TicketUpdate(Guid id,
                                  string Title,
                                  int Number,
                                  string Tag,
                                  string Description,
                                  DateTime FirstAppearance);
