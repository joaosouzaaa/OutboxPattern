using Microsoft.EntityFrameworkCore;
using Tickets.Microservice.Arguments;
using Tickets.Microservice.Data.DatabaseContexts;
using Tickets.Microservice.Data.Repositories.BaseRepositories;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Interfaces.Data.Repositories;
using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Data.Repositories;

public sealed class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
    public TicketRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> AddAsync(Ticket ticket)
    {
        await DbContextSet.AddAsync(ticket);

        return await SaveChangesAsync();
    }

    public Task<bool> UpdateAsync(Ticket ticket)
    {
        _dbContext.Entry(ticket).State = EntityState.Modified;

        return SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Guid id) =>
        DbContextSet.AsNoTracking().AnyAsync(t => t.Id == id);

    public async Task<bool> DeleteAsync(Guid id)
    {
        var ticket = await DbContextSet.FindAsync(id);

        DbContextSet.Remove(ticket!);

        return await SaveChangesAsync();
    }

    public Task<Ticket?> GetByIdAsync(Guid id, bool asNoTracking)
    {
        var query = (IQueryable<Ticket>)DbContextSet;

        if (asNoTracking)
            query = DbContextSet.AsNoTracking();

        return query.FirstOrDefaultAsync(t => t.Id == id);
    }

    public Task<PageList<Ticket>> GetAllPaginatedAsync(GetAllTicketsFilteredArgument filter) =>
        DbContextSet.Where(t => filter.SearchText == null
        || t.Title.ToLower().Contains(filter.SearchText)
        || t.Number.ToString().Contains(filter.SearchText)
        || t.Tag.ToLower().Contains(filter.SearchText))
        .Where(t => filter.StartDate == null || t.CreatedDate >= filter.StartDate)
        .Where(t => filter.EndDate == null || t.CreatedDate <= filter.EndDate)
        .PaginateAsync(filter);

}
