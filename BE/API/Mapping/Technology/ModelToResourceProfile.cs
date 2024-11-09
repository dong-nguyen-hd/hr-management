using API.Resources.DTOs.Technology;

namespace API.Mapping.Technology;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Technology, TechnologyResource>();
        }
}