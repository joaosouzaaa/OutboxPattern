namespace Tickets.Microservice.Entities;

public sealed class Outbox
{
    public Guid Id { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required string Content { get; set; }
    public required string Type { get; set; }
    public DateTime? ProcessedDate { get; set; }
}
