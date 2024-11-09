using API.Extensions;
using API.Resources.DTOs.Technology;

namespace API.Mapping.Technology;

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