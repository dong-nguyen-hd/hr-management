using API.Resources.DTOs.Education;

namespace API.Mapping.Education;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Education, EducationResource>();
        }
}