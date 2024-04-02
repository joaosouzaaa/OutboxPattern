using Support.Microservice.Entities;
using Support.Microservice.Mappers;
using Support.UnitTests.TestBuilders;

namespace Support.UnitTests.MappersTests;
public sealed class SupportEngineerMapperTests
{
    private readonly SupportEngineerMapper _supportEngineerMapper;

    public SupportEngineerMapperTests()
    {
        _supportEngineerMapper = new SupportEngineerMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario_ReturnsDomainEntity()
    {
        // A
        var supportEngineerSave = SupportEngineerBuilder.NewObject().SaveBuild();

        // A
        var supportEngineerResult = _supportEngineerMapper.SaveToDomain(supportEngineerSave);

        // A
        Assert.Equal(supportEngineerResult.Name, supportEngineerSave.Name);
        Assert.Equal(supportEngineerResult.Email, supportEngineerSave.Email);
        Assert.Equal(supportEngineerResult.IsEnabled, supportEngineerSave.IsEnabled);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario_MapsProperly()
    {
        // A
        var supportEngineerUpdate = SupportEngineerBuilder.NewObject().UpdateBuild();
        var supportEngineerResult = SupportEngineerBuilder.NewObject().DomainBuild();

        // A
        _supportEngineerMapper.UpdateToDomain(supportEngineerUpdate, supportEngineerResult);

        // A
        Assert.Equal(supportEngineerResult.Name, supportEngineerUpdate.Name);
        Assert.Equal(supportEngineerResult.Email, supportEngineerUpdate.Email);
        Assert.Equal(supportEngineerResult.IsEnabled, supportEngineerUpdate.IsEnabled);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario_ReturnsResponseList()
    {
        // A
        var supportEngineerList = new List<SupportEngineer>()
        {
            SupportEngineerBuilder.NewObject().DomainBuild(),
            SupportEngineerBuilder.NewObject().DomainBuild(),
            SupportEngineerBuilder.NewObject().DomainBuild(),
            SupportEngineerBuilder.NewObject().DomainBuild()
        };

        // A
        var supportEngineerResponseListResult = _supportEngineerMapper.DomainListToDomainResponse(supportEngineerList);

        // A
        Assert.Equal(supportEngineerResponseListResult.Count, supportEngineerList.Count);
    }
}
