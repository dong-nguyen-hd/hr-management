using Business.Domain.Models;

namespace Business.Domain.Repositories;

public interface ITechnologyRepository : IBaseRepository<Technology>
{
    Task<IEnumerable<Technology>> FindByNameAsync(string filterName);
}