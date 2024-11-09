using AutoMapper;
using Business.Extensions;
using Business.Resources.DTOs.Certificate;

namespace Business.Mapping.Certificate;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateCertificateResource, Domain.Models.Certificate>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()))
                .ForMember(x => x.Provider, opt => opt.MapFrom(src => src.Provider.RemoveSpaceCharacter()));

            CreateMap<UpdateCertificateResource, Domain.Models.Certificate>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()))
                .ForMember(x => x.Provider, opt => opt.MapFrom(src => src.Provider.RemoveSpaceCharacter()));
        }
}