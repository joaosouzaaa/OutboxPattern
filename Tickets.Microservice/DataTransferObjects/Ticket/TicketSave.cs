namespace Tickets.Microservice.DataTransferObjects.Ticket;

public sealed record TicketSave(string Title,
                                int Number,
                                string Tag,
                                string Description,
                                DateTime FirstAppearance);
