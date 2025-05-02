using Business.Domain.Models;

namespace Business.Domain.Repositories;

public interface ICategoryPersonRepository : IBaseRepository<CategoryPerson>
{
    Task<bool> ValidateExistent(int personId, int categoryId);
}