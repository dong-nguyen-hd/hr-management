using API.Resources.DTOs.Department;

namespace API.Mapping.Department;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Department, DepartmentResource>();
        }
}