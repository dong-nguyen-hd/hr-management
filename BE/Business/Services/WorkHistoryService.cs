using AutoMapper;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.DTOs.WorkHistory;
using Microsoft.Extensions.Options;

namespace Business.Services;

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