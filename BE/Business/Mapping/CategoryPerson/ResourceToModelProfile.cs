using AutoMapper;
using Business.Extensions;
using Business.Resources.CategoryPerson;

namespace Business.Mapping.CategoryPerson;

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