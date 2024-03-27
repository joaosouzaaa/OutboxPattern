using Support.Microservice.Entities;

namespace Support.UnitTests.TestBuilders;
public sealed class SupportEngineerBuilder
{
    private readonly long _id = 123;
    private string _name = "test";
    private string _email = "email@test.com";
    private readonly bool _isEnabled = true;

    public static SupportEngineerBuilder NewObject() =>
        new();

    public SupportEngineer DomainBuild() =>
        new()
        {
            Email = _email,
            Id = _id,
            IsEnabled = _isEnabled,
            Name = _name
        };

    public SupportEngineerBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public SupportEngineerBuilder WithEmail(string email)
    {
        _email = email;

        return this;
    }
}
