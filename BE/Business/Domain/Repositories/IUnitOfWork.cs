namespace Business.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task CompleteAsync();
}