using Business.Domain.Models;

namespace Business.Domain.Repositories;

public interface IDepartmentRepository : IBaseRepository<Department>
{
    public Task<List<Department>> FindByNameAsync(string filterName, bool absolute = false);
}