using Microsoft.EntityFrameworkCore.Infrastructure;
using Tickets.Microservice.Data.DatabaseContexts;
using Tickets.Microservice.Interfaces.Data.UnitOfWork;

namespace Tickets.Microservice.Data.UnitOfWork;

public sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    private readonly DatabaseFacade _dbFacade = dbContext.Database;

    public void BeginTransaction() =>
        _dbFacade.BeginTransaction();

    public void CommitTransaction()
    {
        try
        {
            _dbFacade.CommitTransaction();
        }
        catch
        {
            RollbackTransaction();

            throw;
        }
    }

    public void RollbackTransaction() =>
        _dbFacade.RollbackTransaction();
}
