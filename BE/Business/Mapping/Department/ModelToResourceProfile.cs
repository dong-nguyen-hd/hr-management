using AutoMapper;
using Business.Resources.Department;

namespace Business.Mapping.Department;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Department, DepartmentResource>();
        }
}