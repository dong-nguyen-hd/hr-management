using API.Extensions;
using API.Resources.DTOs.Category;

namespace API.Mapping.Category;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateCategoryResource, Domain.Models.Category>()
                .ForMember(x => x.Technologies, opt => opt.MapFrom(src => src.Technologies))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));

            CreateMap<UpdateCategoryResource, Domain.Models.Category>()
                .ForMember(x => x.Technologies, opt => opt.MapFrom(src => src.Technologies))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));
        }
}