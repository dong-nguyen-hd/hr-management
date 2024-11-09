using API.Resources.DTOs.CategoryPerson;

namespace API.Mapping.CategoryPerson;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.CategoryPerson, CategoryPersonResource>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(x => x.Technologies, opt => opt.Ignore());
        }
}