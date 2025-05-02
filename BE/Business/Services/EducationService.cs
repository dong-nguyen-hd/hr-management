using AutoMapper;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.Education;
using Microsoft.Extensions.Options;

namespace Business.Services;

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