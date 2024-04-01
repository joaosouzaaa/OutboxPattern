namespace Support.Microservice.DataTransferObjects.SupportEngineer;

public sealed class SupportEngineerResponse
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required bool IsEnabled { get; set; }
}
