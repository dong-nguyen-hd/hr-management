using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Certificate;
using Microsoft.Extensions.Options;

namespace API.Services;

public class CertificateService : BaseService<CertificateResource, CreateCertificateResource, UpdateCertificateResource, Certificate>, ICertificateService
{
    #region Constructor
    public CertificateService(ICertificateRepository certificateRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(certificateRepository, mapper, unitOfWork, responseMessage)
    {
        }
    #endregion
}