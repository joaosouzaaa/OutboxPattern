using Microsoft.Extensions.Configuration;
using MimeKit;
using Moq;
using Support.Microservice.Contracts;
using Support.Microservice.Interfaces.Repositories;
using Support.Microservice.Interfaces.Settings;
using Support.Microservice.Services;

namespace Support.UnitTests.ServicesTests;
public sealed class EmailServiceTests
{
    private readonly Mock<ISupportEngineerRepository> _supportEngineerRepositoryMock;
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly IConfiguration _configuration;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        _supportEngineerRepositoryMock = new Mock<ISupportEngineerRepository>();
        _emailSenderMock = new Mock<IEmailSender>();
        var inMemoryCollection = new Dictionary<string, string>()
        {
            {"EmailCredentials:From", "test" }
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryCollection!)
            .Build();
        _emailService = new EmailService(_supportEngineerRepositoryMock.Object, _emailSenderMock.Object, _configuration);
    }

    [Fact]
    public async Task SendTicketCreatedEmailAsync_SuccessfulScenario()
    {
        // A
        var ticketCreatedEvent = new TicketCreatedEvent(Guid.NewGuid(), 123);

        var toEmailList = new List<string>()
        {
            "valid@test.com",
            "joao@test.com"
        };
        _supportEngineerRepositoryMock.Setup(s => s.GetAllEmailsEnabledAsync())
            .ReturnsAsync(toEmailList);

        _emailSenderMock.Setup(e => e.SendEmailAsync(It.Is<MimeMessage>(m => m.To.Count == toEmailList.Count)));

        // A
        await _emailService.SendTicketCreatedEmailAsync(ticketCreatedEvent);

        // A
        _emailSenderMock.Verify(e => e.SendEmailAsync(It.Is<MimeMessage>(m => m.To.Count == toEmailList.Count)), Times.Once());

    }
}
