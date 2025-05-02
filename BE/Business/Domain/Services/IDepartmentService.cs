using Business.Domain.Models;
using Business.Resources.Department;

namespace Business.Domain.Services;

public interface IDepartmentService : IBaseService<DepartmentResource, CreateDepartmentResource, UpdateDepartmentResource, Department>
{
}