using Support.Microservice.Entities;

namespace Support.Microservice.Interfaces.Repositories;

public interface ISupportEngineerRepository
{
    Task<bool> AddAsync(SupportEngineer supportEngineer);
    Task<SupportEngineer?> GetByIdAsync(long id);
    Task<bool> UpdateAsync(SupportEngineer supportEngineer);
    Task<List<SupportEngineer>> GetAllAsync();
    Task<List<string>> GetAllEmailsEnabledAsync();
}
