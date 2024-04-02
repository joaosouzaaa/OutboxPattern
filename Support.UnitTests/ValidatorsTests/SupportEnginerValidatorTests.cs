using Support.Microservice.Validators;
using Support.UnitTests.TestBuilders;

namespace Support.UnitTests.ValidatorsTests;
public sealed class SupportEnginerValidatorTests
{
    private readonly SupportEngineerValidator _validator;

    public SupportEnginerValidatorTests()
    {
        _validator = new SupportEngineerValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var supportEnginnerToValidate = SupportEngineerBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _validator.ValidateAsync(supportEnginnerToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var supportEnginnerWithInvalidName= SupportEngineerBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _validator.ValidateAsync(supportEnginnerWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            "",
            "a",
            new string('a', 201)
        };

    [Theory]
    [MemberData(nameof(InvalidEmailParameters))]
    public async Task ValidateAsync_InvalidEmail_ReturnsFalse(string email)
    {
        // A
        var supportEnginnerWithInvalidEmail = SupportEngineerBuilder.NewObject().WithEmail(email).DomainBuild();

        // A
        var validationResult = await _validator.ValidateAsync(supportEnginnerWithInvalidEmail);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidEmailParameters() =>
        new()
        {
            "",
            "a",
            new string('a', 201),
            "a@t.c",
            $"valid{new string('a', 201)}@email.com"
        };
}
