using FluentValidation;
using FluentValidation.Results;
using Moq;
using Support.Microservice.DataTransferObjects.SupportEngineer;
using Support.Microservice.Entities;
using Support.Microservice.Interfaces.Mappers;
using Support.Microservice.Interfaces.Repositories;
using Support.Microservice.Interfaces.Settings;
using Support.Microservice.Services;
using Support.UnitTests.TestBuilders;

namespace Support.UnitTests.ServicesTests;
public sealed class SupportEngineerServiceTests
{
    private readonly Mock<ISupportEngineerRepository> _supportEngineerRepositoryMock;
    private readonly Mock<ISupportEngineerMapper> _supportEngineerMapperMock;
    private readonly Mock<IValidator<SupportEngineer>> _validatorMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly SupportEngineerService _supportEngineerService;

    public SupportEngineerServiceTests()
    {
        _supportEngineerRepositoryMock = new Mock<ISupportEngineerRepository>();
        _supportEngineerMapperMock = new Mock<ISupportEngineerMapper>();
        _validatorMock = new Mock<IValidator<SupportEngineer>>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _supportEngineerService = new SupportEngineerService(_supportEngineerRepositoryMock.Object, _supportEngineerMapperMock.Object,
            _validatorMock.Object, _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var supportEngineerSave = SupportEngineerBuilder.NewObject().SaveBuild();

        var supportEngineer = SupportEngineerBuilder.NewObject().DomainBuild();
        _supportEngineerMapperMock.Setup(s => s.SaveToDomain(It.IsAny<SupportEngineerSave>()))
            .Returns(supportEngineer);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<SupportEngineer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _supportEngineerRepositoryMock.Setup(s => s.AddAsync(It.IsAny<SupportEngineer>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _supportEngineerService.AddAsync(supportEngineerSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _supportEngineerRepositoryMock.Verify(s => s.AddAsync(It.IsAny<SupportEngineer>()), Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var supportEngineerSave = SupportEngineerBuilder.NewObject().SaveBuild();

        var supportEngineer = SupportEngineerBuilder.NewObject().DomainBuild();
        _supportEngineerMapperMock.Setup(s => s.SaveToDomain(It.IsAny<SupportEngineerSave>()))
            .Returns(supportEngineer);

        var validationFailureList = new List<ValidationFailure>()
        {
            new ("ates", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<SupportEngineer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addResult = await _supportEngineerService.AddAsync(supportEngineerSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _supportEngineerRepositoryMock.Verify(s => s.AddAsync(It.IsAny<SupportEngineer>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var supportEngineerUpdate = SupportEngineerBuilder.NewObject().UpdateBuild();

        var supportEngineer = SupportEngineerBuilder.NewObject().DomainBuild();
        _supportEngineerRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(supportEngineer);

        _supportEngineerMapperMock.Setup(s => s.UpdateToDomain(It.IsAny<SupportEngineerUpdate>(), It.IsAny<SupportEngineer>()));

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<SupportEngineer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _supportEngineerRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<SupportEngineer>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _supportEngineerService.UpdateAsync(supportEngineerUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _supportEngineerRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<SupportEngineer>()), Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var supportEngineerUpdate = SupportEngineerBuilder.NewObject().UpdateBuild();

        _supportEngineerRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<long>()))
            .Returns(Task.FromResult<SupportEngineer?>(null));

        // A
        var updateResult = await _supportEngineerService.UpdateAsync(supportEngineerUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _supportEngineerMapperMock.Verify(s => s.UpdateToDomain(It.IsAny<SupportEngineerUpdate>(), It.IsAny<SupportEngineer>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<SupportEngineer>(), It.IsAny<CancellationToken>()), Times.Never());
        _supportEngineerRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<SupportEngineer>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var supportEngineerUpdate = SupportEngineerBuilder.NewObject().UpdateBuild();

        var supportEngineer = SupportEngineerBuilder.NewObject().DomainBuild();
        _supportEngineerRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(supportEngineer);

        _supportEngineerMapperMock.Setup(s => s.UpdateToDomain(It.IsAny<SupportEngineerUpdate>(), It.IsAny<SupportEngineer>()));

        var validationFailureList = new List<ValidationFailure>()
        {
            new ("ates", "test"),
            new ("ates", "test"),
            new ("ates", "test"),
            new ("ates", "test")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<SupportEngineer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var updateResult = await _supportEngineerService.UpdateAsync(supportEngineerUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _supportEngineerRepositoryMock.Verify(s => s.UpdateAsync(It.IsAny<SupportEngineer>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsEntityList()
    {
        // A
        var supportEngineerList = new List<SupportEngineer>()
        {
            SupportEngineerBuilder.NewObject().DomainBuild(),
            SupportEngineerBuilder.NewObject().DomainBuild()
        };
        _supportEngineerRepositoryMock.Setup(s => s.GetAllAsync())
            .ReturnsAsync(supportEngineerList);

        var supportEngineerResponseList = new List<SupportEngineerResponse>()
        {
            SupportEngineerBuilder.NewObject().ResponseBuild()
        };
        _supportEngineerMapperMock.Setup(s => s.DomainListToDomainResponse(It.IsAny<List<SupportEngineer>>()))
            .Returns(supportEngineerResponseList);

        // A
        var supportEngineerResponseListResult = await _supportEngineerService.GetAllAsync();

        // A
        Assert.Equal(supportEngineerResponseListResult.Count, supportEngineerResponseList.Count);
    }
}
