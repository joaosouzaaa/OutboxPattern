using Support.Microservice.DataTransferObjects.SupportEngineer;
using Support.Microservice.Entities;
using Support.Microservice.Interfaces.Mappers;

namespace Support.Microservice.Mappers;

public sealed class SupportEngineerMapper : ISupportEngineerMapper
{
    public SupportEngineer SaveToDomain(SupportEngineerSave supportEngineerSave) =>
        new()
        {
            Name = supportEngineerSave.Name,
            Email = supportEngineerSave.Email,
            IsEnabled = supportEngineerSave.IsEnabled
        };

    public void UpdateToDomain(SupportEngineerUpdate supportEngineerUpdate, SupportEngineer supportEngineer)
    {
        supportEngineer.Name = supportEngineerUpdate.Name;
        supportEngineer.Email = supportEngineerUpdate.Email;
        supportEngineer.IsEnabled = supportEngineerUpdate.IsEnabled;
    }

    public List<SupportEngineerResponse> DomainListToDomainResponse(List<SupportEngineer> supportEngineerList) =>
        supportEngineerList.Select(DomainToResponse).ToList();

    private SupportEngineerResponse DomainToResponse(SupportEngineer supportEngineer) =>
        new()
        {
            Id = supportEngineer.Id,
            Name = supportEngineer.Name,
            Email = supportEngineer.Email,
            IsEnabled = supportEngineer.IsEnabled
        };
}
