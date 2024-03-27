using Microsoft.EntityFrameworkCore;
using Support.Microservice.Data.DatabaseContexts;
using Support.Microservice.Entities;
using Support.Microservice.Interfaces.Repositories;

namespace Support.Microservice.Data.Repositories;

public sealed class SupportEngineerRepository(AppDbContext dbContext) : ISupportEngineerRepository, IDisposable
{
    private DbSet<SupportEngineer> DbContextSet => dbContext.Set<SupportEngineer>();

    public async Task<bool> AddAsync(SupportEngineer supportEngineer)
    {
        await DbContextSet.AddAsync(supportEngineer);

        return await SaveChangesAsync();
    }

    public Task<bool> UpdateAsync(SupportEngineer supportEngineer)
    {
        dbContext.Entry(supportEngineer).State = EntityState.Modified;

        return SaveChangesAsync();
    }

    public Task<List<SupportEngineer>> GetAllAsync() =>
        DbContextSet.AsNoTracking().ToListAsync();

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    private async Task<bool> SaveChangesAsync() =>
        await dbContext.SaveChangesAsync() > 0;
}
