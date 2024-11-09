using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Education;
using Microsoft.Extensions.Options;

namespace API.Services;

public class EducationService : BaseService<EducationResource, CreateEducationResource, UpdateEducationResource, Education>, IEducationService
{
    #region Constructor
    public EducationService(IEducationRepository educationRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(educationRepository, mapper, unitOfWork, responseMessage)
    {
        }
    #endregion
}