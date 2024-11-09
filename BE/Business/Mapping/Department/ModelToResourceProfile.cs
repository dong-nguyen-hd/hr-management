using AutoMapper;
using Business.Resources.DTOs.Department;

namespace Business.Mapping.Department;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Department, DepartmentResource>();
        }
}