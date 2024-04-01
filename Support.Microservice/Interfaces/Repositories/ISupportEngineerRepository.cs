using Support.Microservice.Entities;

namespace Support.Microservice.Interfaces.Repositories;

public interface ISupportEngineerRepository
{
    Task<bool> AddAsync(SupportEngineer supportEngineer);
    Task<bool> UpdateAsync(SupportEngineer supportEngineer);
    Task<List<SupportEngineer>> GetAllAsync();
    Task<List<SupportEngineer>> GetAllEnabledAsync();
}
