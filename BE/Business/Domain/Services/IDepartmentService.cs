using Business.Domain.Models;
using Business.Resources.DTOs.Department;

namespace Business.Domain.Services;

public interface IDepartmentService : IBaseService<DepartmentResource, CreateDepartmentResource, UpdateDepartmentResource, Department>
{
}