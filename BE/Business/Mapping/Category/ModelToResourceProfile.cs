using AutoMapper;
using Business.Resources.Category;

namespace Business.Mapping.Category;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Category, CategoryResource>();
        }
}