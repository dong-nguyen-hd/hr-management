using API.Extensions;
using API.Resources.DTOs.CategoryPerson;

namespace API.Mapping.CategoryPerson;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateCategoryPersonResource, Domain.Models.CategoryPerson>()
                .ForMember(x => x.Technologies, opt => opt.MapFrom(src => src.Technologies.ConcatenateWithComma()));

            CreateMap<UpdateCategoryPersonResource, Domain.Models.CategoryPerson>()
                .ForMember(x => x.Technologies, opt => opt.MapFrom(src => src.Technologies.ConcatenateWithComma()));
        }
}