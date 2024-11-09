using API.Domain.Models;
using API.Resources.DTOs.Certificate;

namespace API.Domain.Services;

public interface ICertificateService : IBaseService<CertificateResource, CreateCertificateResource, UpdateCertificateResource, Certificate>
{
}