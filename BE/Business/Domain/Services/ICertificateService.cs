using Business.Domain.Models;
using Business.Resources.DTOs.Certificate;

namespace Business.Domain.Services;

public interface ICertificateService : IBaseService<CertificateResource, CreateCertificateResource, UpdateCertificateResource, Certificate>
{
}