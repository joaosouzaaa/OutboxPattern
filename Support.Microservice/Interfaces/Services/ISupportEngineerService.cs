using Support.Microservice.DataTransferObjects.SupportEngineer;

namespace Support.Microservice.Interfaces.Services;

public interface ISupportEngineerService
{
    Task<bool> AddAsync(SupportEngineerSave supportEngineerSave);
    Task<bool> UpdateAsync(SupportEngineerUpdate supportEngineerUpdate);
    Task<List<SupportEngineerResponse>> GetAllAsync();
}
