using Microsoft.EntityFrameworkCore;
using Tickets.Microservice.Data.DatabaseContexts;
using Tickets.Microservice.Data.Repositories.BaseRepositories;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Data.Repositories;

namespace Tickets.Microservice.Data.Repositories;

public sealed class OutboxRepository(AppDbContext dbContext) : BaseRepository<Outbox>(dbContext), IOutboxRepository
{
    public async Task<bool> AddAsync(Outbox outbox)
    {
        await DbContextSet.AddAsync(outbox);

        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateProcessedDateAsync(Guid id, DateTime processedDate) =>
        await DbContextSet.Where(o => o.Id == id)
                          .ExecuteUpdateAsync(o => o.SetProperty(o => o.ProcessedDate, processedDate)) > 0;

    public Task<List<Outbox>> GetAllUnprocessedMessagesAsync() =>
        DbContextSet.Where(o => o.ProcessedDate == null)
                    .OrderBy(o => o.CreatedDate)
                    .AsNoTracking()
                    .ToListAsync();
}
