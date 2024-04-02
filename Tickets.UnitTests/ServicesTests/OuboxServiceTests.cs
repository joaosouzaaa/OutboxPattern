using Moq;
using System.Text.Json;
using Tickets.Microservice.Contracts;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Data.Repositories;
using Tickets.Microservice.Services;

namespace Tickets.UnitTests.ServicesTests;
public sealed class OuboxServiceTests
{
    private readonly Mock<IOutboxRepository> _outboxRepositoryMock;
    private readonly OutboxService _outboxService;

    public OuboxServiceTests()
    {
        _outboxRepositoryMock = new Mock<IOutboxRepository>();
        _outboxService = new OutboxService(_outboxRepositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTask()
    {
        // A
        var ticketCreatedEvent = new TicketCreatedEvent(Guid.NewGuid(), 123);

        _outboxRepositoryMock.Setup(o => o.AddAsync(It.IsAny<Outbox>()));

        // A
        await _outboxService.AddAsync(ticketCreatedEvent);

        // A
        _outboxRepositoryMock.Verify(o => o.AddAsync(It.Is<Outbox>(o => o.Type == nameof(TicketCreatedEvent))), Times.Once());
    }
}
