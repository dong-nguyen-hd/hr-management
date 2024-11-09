using API.Domain.Models;
using API.Resources;
using API.Resources.DTOs.Group;
using API.Resources.DTOs.Person;
using API.Results;

namespace API.Domain.Services;

public interface IGroupService : IBaseService<GroupResource, CreateGroupResource, UpdateGroupResource, Group>
{
    Task<PaginationResult<IEnumerable<GroupResource>>> GetPaginationAsync(QueryResource pagination, FilterGroupResource filterResource);
    Task<BaseResult<GroupResource>> AddGroupToAccountAsync(int accountId, int groupId);
    Task<BaseResult<GroupResource>> RemoveGroupFromAccountAsync(int accountId, int groupId);
    Task<BaseResult<IEnumerable<PersonResource>>> GetListPersonByGroupIdAsync(int groupId);
}