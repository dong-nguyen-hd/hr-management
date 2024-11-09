using API.Domain.Models;
using API.Resources.DTOs.WorkHistory;

namespace API.Domain.Services;

public interface IWorkHistoryService : IBaseService<WorkHistoryResource, CreateWorkHistoryResource, UpdateWorkHistoryResource, WorkHistory>
{
}