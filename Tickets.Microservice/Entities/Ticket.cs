namespace Tickets.Microservice.Entities;

public sealed class Ticket
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required int Number { get; set; }
    public required string Tag { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime FirstAppearance { get; set; }
}
