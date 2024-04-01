namespace Support.Microservice.DataTransferObjects.SupportEngineer;

public sealed record SupportEngineerSave(string Name,
                                         string Email,
                                         bool IsEnabled);
