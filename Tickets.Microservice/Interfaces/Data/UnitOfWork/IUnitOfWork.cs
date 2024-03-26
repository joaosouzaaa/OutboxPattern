namespace Tickets.Microservice.Interfaces.Data.UnitOfWork;

public interface IUnitOfWork
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
