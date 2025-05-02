using Business.Domain.Models;
using Business.Resources.Certificate;

namespace Business.Domain.Services;

public interface ICertificateService : IBaseService<CertificateResource, CreateCertificateResource, UpdateCertificateResource, Certificate>
{
}