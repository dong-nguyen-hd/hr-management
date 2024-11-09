using API.Domain.Models;
using API.Resources.DTOs.Department;

namespace API.Domain.Services;

public interface IDepartmentService : IBaseService<DepartmentResource, CreateDepartmentResource, UpdateDepartmentResource, Department>
{
}