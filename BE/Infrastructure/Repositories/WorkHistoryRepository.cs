using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class WorkHistoryRepository(CoreContext context) : BaseRepository<WorkHistory>(context), IWorkHistoryRepository
{
}