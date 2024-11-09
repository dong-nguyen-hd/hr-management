using API.Resources.DTOs.Category;

namespace API.Mapping.Category;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Category, CategoryResource>();
        }
}