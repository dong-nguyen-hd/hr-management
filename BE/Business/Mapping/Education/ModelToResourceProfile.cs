using AutoMapper;
using Business.Resources.Education;

namespace Business.Mapping.Education;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Education, EducationResource>();
        }
}