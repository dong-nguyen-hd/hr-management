using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class WorkHistoryRepository : BaseRepository<WorkHistory>, IWorkHistoryRepository
{
    #region Constructor
    public WorkHistoryRepository(AppDbContext context) : base(context) { }
    #endregion
}