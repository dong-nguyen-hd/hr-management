using AutoMapper;
using Business.Resources.Certificate;

namespace Business.Mapping.Certificate;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Certificate, CertificateResource>();
        }
}