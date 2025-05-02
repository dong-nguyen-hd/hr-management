using Business.Domain.Models;

namespace Business.Domain.Repositories;

public interface IPayRepository : IBaseRepository<Pay>
{
    Task<Pay> GetByPersonIdAsync(int personId, DateTime date);
}