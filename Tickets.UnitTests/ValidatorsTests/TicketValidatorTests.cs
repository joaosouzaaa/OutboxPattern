using Tickets.Microservice.Validators;
using Tickets.UnitTests.TestBuilders;

namespace Tickets.UnitTests.ValidatorsTests;
public sealed class TicketValidatorTests
{
    private readonly TicketValidator _ticketValidator;

    public TicketValidatorTests()
    {
        _ticketValidator = new TicketValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var ticketToValidate = TicketBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _ticketValidator.ValidateAsync(ticketToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidTitleParameters))]
    public async Task ValidateAsync_InvalidTitle_ReturnsFalse(string title)
    {
        // A
        var ticketWithInvalidTitle = TicketBuilder.NewObject().WithTitle(title).DomainBuild();

        // A
        var validationResult = await _ticketValidator.ValidateAsync(ticketWithInvalidTitle);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidTitleParameters() =>
        new()
        {
            "",
            new string('a', 151),
            "aa"
        };

    [Theory]
    [MemberData(nameof(InvalidNumberParameters))]
    public async Task ValidateAsync_InvalidNumber_ReturnsFalse(int number)
    {
        // A
        var ticketWithInvalidNumber = TicketBuilder.NewObject().WithNumber(number).DomainBuild();

        // A
        var validationResult = await _ticketValidator.ValidateAsync(ticketWithInvalidNumber);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<int> InvalidNumberParameters() =>
        new()
        {
            0,
            -5
        };

    [Theory]
    [MemberData(nameof(InvalidTagParameters))]
    public async Task ValidateAsync_InvalidTag_ReturnsFalse(string tag)
    {
        // A
        var ticketWithInvalidTag = TicketBuilder.NewObject().WithTag(tag).DomainBuild();

        // A
        var validationResult = await _ticketValidator.ValidateAsync(ticketWithInvalidTag);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidTagParameters() =>
        new()
        {
            "",
            new string('a', 151)
        };

    [Theory]
    [MemberData(nameof(InvalidDescriptionParameters))]
    public async Task ValidateAsync_InvalidDescription_ReturnsFalse(string description)
    {
        // A
        var ticketWithInvalidDescription = TicketBuilder.NewObject().WithDescription(description).DomainBuild();

        // A
        var validationResult = await _ticketValidator.ValidateAsync(ticketWithInvalidDescription);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidDescriptionParameters() =>
        new()
        {
            "",
            new string('a', 2001),
            "aaaa",
            "test",
            "random"
        };
}
