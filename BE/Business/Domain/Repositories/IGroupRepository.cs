using Business.Domain.Models;
using Business.Resources;
using Business.Resources.Group;

namespace Business.Domain.Repositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<IEnumerable<Group>> FindByNameAsync(string filterName);
    Task<(IEnumerable<Group> records, int total)> GetPaginationAsync(QueryResource pagination, FilterGroupResource filterResource);
    Task<IEnumerable<Person>> GetListPersonByGroupIdAsync(int groupId);
}