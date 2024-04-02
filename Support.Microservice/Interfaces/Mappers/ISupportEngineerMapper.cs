using Support.Microservice.DataTransferObjects.SupportEngineer;
using Support.Microservice.Entities;

namespace Support.Microservice.Interfaces.Mappers;

public interface ISupportEngineerMapper
{
    SupportEngineer SaveToDomain(SupportEngineerSave supportEngineerSave);
    void UpdateToDomain(SupportEngineerUpdate supportEngineerUpdate, SupportEngineer supportEngineer);
    List<SupportEngineerResponse> DomainListToDomainResponse(List<SupportEngineer> supportEngineerList);
}
