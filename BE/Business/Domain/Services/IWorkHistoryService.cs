using Business.Domain.Models;
using Business.Resources.WorkHistory;

namespace Business.Domain.Services;

public interface IWorkHistoryService : IBaseService<WorkHistoryResource, CreateWorkHistoryResource, UpdateWorkHistoryResource, WorkHistory>
{
}