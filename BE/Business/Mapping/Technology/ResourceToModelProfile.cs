using AutoMapper;
using Business.Extensions;
using Business.Resources.Technology;

namespace Business.Mapping.Technology;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateTechnologyResource, Domain.Models.Technology>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));

            CreateMap<UpdateTechnologyResource, Domain.Models.Technology>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));
        }
}