using Business.Domain.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    #region Property

    private readonly CoreContext _context;
    private bool _disposed;

    #endregion

    #region Constructor

    public UnitOfWork(CoreContext context) => this._context = context;

    #endregion

    #region Method

    public async Task CompleteAsync() =>
        await _context.SaveChangesAsync();

    protected virtual void Clean(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        this._disposed = true;
    }

    public void Dispose()
    {
        Clean(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}