using Business.Domain.Models;
using Business.Resources.DTOs.WorkHistory;

namespace Business.Domain.Services;

public interface IWorkHistoryService : IBaseService<WorkHistoryResource, CreateWorkHistoryResource, UpdateWorkHistoryResource, WorkHistory>
{
}