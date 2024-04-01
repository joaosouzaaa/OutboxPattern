namespace Support.Microservice.DataTransferObjects.SupportEngineer;

public sealed record SupportEngineerUpdate(long Id,
                                           string Name,
                                           string Email,
                                           bool IsEnabled);
