using API.Resources.DTOs.Certificate;

namespace API.Mapping.Certificate;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Certificate, CertificateResource>();
        }
}