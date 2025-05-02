using Business.Domain.Models;

namespace Business.Domain.Repositories;

public interface IPositionRepository : IBaseRepository<Position>
{
    public Task<List<Position>> FindByNameAsync(string filterName, bool absolute = false);
}