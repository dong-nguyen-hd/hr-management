using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.WorkHistory;
using Microsoft.Extensions.Options;

namespace API.Services;

public class WorkHistoryService : BaseService<WorkHistoryResource, CreateWorkHistoryResource, UpdateWorkHistoryResource, WorkHistory>, IWorkHistoryService
{
    #region Constructor
    public WorkHistoryService(IWorkHistoryRepository workHistoryRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(workHistoryRepository, mapper, unitOfWork, responseMessage)
    {
        }
    #endregion
}