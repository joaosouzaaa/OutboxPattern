namespace Support.Microservice.Entities;

public sealed class SupportEngineer
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required bool IsEnabled { get; set; }
}
