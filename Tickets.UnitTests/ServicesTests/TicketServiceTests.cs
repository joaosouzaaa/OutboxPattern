using FluentValidation;
using FluentValidation.Results;
using Moq;
using Tickets.Microservice.Arguments;
using Tickets.Microservice.Contracts;
using Tickets.Microservice.DataTransferObjects.Ticket;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Data.Repositories;
using Tickets.Microservice.Interfaces.Mappers;
using Tickets.Microservice.Interfaces.Services;
using Tickets.Microservice.Interfaces.Settings;
using Tickets.Microservice.Services;
using Tickets.Microservice.Settings.PaginationSettings;
using Tickets.UnitTests.TestBuilders;

namespace Tickets.UnitTests.ServicesTests;
public sealed class TicketServiceTests
{
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<ITicketMapper> _ticketMapperMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<Ticket>> _validatorMock;
    private readonly Mock<IOutboxService> _outboxServiceMock;
    private readonly TicketService _ticketService;

    public TicketServiceTests()
    {
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _ticketMapperMock = new Mock<ITicketMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<Ticket>>();
        _outboxServiceMock = new Mock<IOutboxService>();
        _ticketService = new TicketService(_ticketRepositoryMock.Object, _ticketMapperMock.Object, _notificationHandlerMock.Object,
            _validatorMock.Object, _outboxServiceMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var ticketSave = TicketBuilder.NewObject().SaveBuild();

        var ticket = TicketBuilder.NewObject().DomainBuild();
        _ticketMapperMock.Setup(t => t.SaveToDomain(It.IsAny<TicketSave>()))
            .Returns(ticket);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Ticket>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _ticketRepositoryMock.Setup(t => t.AddAsync(It.IsAny<Ticket>()))
            .ReturnsAsync(true);

        _outboxServiceMock.Setup(o => o.AddAsync(It.IsAny<TicketCreatedEvent>()));

        // A
        var addResult = await _ticketService.AddAsync(ticketSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _ticketRepositoryMock.Verify(t => t.AddAsync(It.IsAny<Ticket>()), Times.Once());
        _outboxServiceMock.Verify(o => o.AddAsync(It.IsAny<TicketCreatedEvent>()), Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var ticketSave = TicketBuilder.NewObject().SaveBuild();

        var ticket = TicketBuilder.NewObject().DomainBuild();
        _ticketMapperMock.Setup(t => t.SaveToDomain(It.IsAny<TicketSave>()))
            .Returns(ticket);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Ticket>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addResult = await _ticketService.AddAsync(ticketSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _ticketRepositoryMock.Verify(t => t.AddAsync(It.IsAny<Ticket>()), Times.Never());
        _outboxServiceMock.Verify(o => o.AddAsync(It.IsAny<TicketCreatedEvent>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var ticketUpdate = TicketBuilder.NewObject().UpdateBuild();

        var ticket = TicketBuilder.NewObject().DomainBuild();
        _ticketRepositoryMock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(ticket);

        _ticketMapperMock.Setup(t => t.UpdateToDomain(It.IsAny<TicketUpdate>(), It.IsAny<Ticket>()));

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Ticket>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _ticketRepositoryMock.Setup(t => t.UpdateAsync(It.IsAny<Ticket>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _ticketService.UpdateAsync(ticketUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _ticketRepositoryMock.Verify(t => t.UpdateAsync(It.IsAny<Ticket>()), Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var ticketUpdate = TicketBuilder.NewObject().UpdateBuild();

        _ticketRepositoryMock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>(), false))
            .Returns(Task.FromResult<Ticket?>(null));

        // A
        var updateResult = await _ticketService.UpdateAsync(ticketUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _ticketMapperMock.Verify(t => t.UpdateToDomain(It.IsAny<TicketUpdate>(), It.IsAny<Ticket>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Ticket>(), It.IsAny<CancellationToken>()), Times.Never());
        _ticketRepositoryMock.Verify(t => t.UpdateAsync(It.IsAny<Ticket>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var ticketUpdate = TicketBuilder.NewObject().UpdateBuild();

        var ticket = TicketBuilder.NewObject().DomainBuild();
        _ticketRepositoryMock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(ticket);

        _ticketMapperMock.Setup(t => t.UpdateToDomain(It.IsAny<TicketUpdate>(), It.IsAny<Ticket>()));

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "test"),
            new("test", "test"),
            new("test", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Ticket>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var updateResult = await _ticketService.UpdateAsync(ticketUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _ticketRepositoryMock.Verify(t => t.UpdateAsync(It.IsAny<Ticket>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = Guid.NewGuid();

        _ticketRepositoryMock.Setup(t => t.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _ticketRepositoryMock.Setup(t => t.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // A
        var deleteResult = await _ticketService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _ticketRepositoryMock.Verify(t => t.DeleteAsync(It.IsAny<Guid>()), Times.Once());

        Assert.True(deleteResult);
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = Guid.NewGuid();

        _ticketRepositoryMock.Setup(t => t.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // A
        var deleteResult = await _ticketService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _ticketRepositoryMock.Verify(t => t.DeleteAsync(It.IsAny<Guid>()), Times.Never());

        Assert.False(deleteResult);
    }

    [Fact]
    public async Task GetbyIdAsync_SuccessfulScenario_ReturnsEntity()
    {
        // A
        var id = Guid.NewGuid();

        var ticket = TicketBuilder.NewObject().DomainBuild();
        _ticketRepositoryMock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>(), true))
            .ReturnsAsync(ticket);

        var ticketResponse = TicketBuilder.NewObject().ResponseBuild();
        _ticketMapperMock.Setup(t => t.DomainToResponse(It.IsAny<Ticket>()))
            .Returns(ticketResponse);

        // A
        var ticketResponseResult = await _ticketService.GetByIdAsync(id);

        // A
        _ticketMapperMock.Verify(t => t.DomainToResponse(It.IsAny<Ticket>()), Times.Once());

        Assert.NotNull(ticketResponseResult);
    }

    [Fact]
    public async Task GetbyIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = Guid.NewGuid();

        _ticketRepositoryMock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>(), true))
            .Returns(Task.FromResult<Ticket?>(null));

        // A
        var ticketResponseResult = await _ticketService.GetByIdAsync(id);

        // A
        _ticketMapperMock.Verify(t => t.DomainToResponse(It.IsAny<Ticket>()), Times.Never());

        Assert.Null(ticketResponseResult);
    }

    [Fact]
    public async Task GetAllPaginatedAsync_SuccessfulScenario_ReturnsEntityPageList()
    {
        // A
        var filter = new GetAllTicketsFilteredArgument()
        {
            PageNumber = 12,
            PageSize = 1,
        };

        var ticketList = new List<Ticket>()
        {
            TicketBuilder.NewObject().DomainBuild(),
            TicketBuilder.NewObject().DomainBuild(),
            TicketBuilder.NewObject().DomainBuild()
        };
        var ticketPageList = new PageList<Ticket>()
        {
            CurrentPage = 1,
            PageSize = 12,
            Result = ticketList,
            TotalCount = 9,
            TotalPages = 9
        };
        _ticketRepositoryMock.Setup(t => t.GetAllPaginatedAsync(It.IsAny<GetAllTicketsFilteredArgument>()))
            .ReturnsAsync(ticketPageList);

        var ticketResponseList = new List<TicketResponse>()
        {
            TicketBuilder.NewObject().ResponseBuild(),
            TicketBuilder.NewObject().ResponseBuild()
        };
        var ticketResponsePageList = new PageList<TicketResponse>()
        {
            CurrentPage = 1,
            PageSize = 123,
            Result = ticketResponseList,
            TotalCount = 1,
            TotalPages = 8
        };
        _ticketMapperMock.Setup(t => t.DomainPageListToResponsePageList(It.IsAny<PageList<Ticket>>()))
            .Returns(ticketResponsePageList);

        // A
        var ticketResponsePageListResult = await _ticketService.GetAllPaginatedAsync(filter);

        // A
        Assert.Equal(ticketResponsePageListResult.Result.Count, ticketResponsePageList.Result.Count);
    }
}
