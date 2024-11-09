using AutoMapper;
using Business.Resources.DTOs.Technology;

namespace Business.Mapping.Technology;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Technology, TechnologyResource>();
        }
}