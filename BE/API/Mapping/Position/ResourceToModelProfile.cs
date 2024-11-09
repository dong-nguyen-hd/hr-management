using API.Extensions;
using API.Resources.DTOs.Position;

namespace API.Mapping.Position;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreatePositionResource, Domain.Models.Position>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));

            CreateMap<UpdatePositionResource, Domain.Models.Position>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));
        }
}