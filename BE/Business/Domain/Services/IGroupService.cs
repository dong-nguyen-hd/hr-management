using Business.Domain.Models;
using Business.Resources;
using Business.Resources.DTOs.Group;
using Business.Resources.DTOs.Person;
using Business.Results;

namespace Business.Domain.Services;

public interface IGroupService : IBaseService<GroupResource, CreateGroupResource, UpdateGroupResource, Group>
{
    Task<PaginationResult<IEnumerable<GroupResource>>> GetPaginationAsync(QueryResource pagination, FilterGroupResource filterResource);
    Task<BaseResult<GroupResource>> AddGroupToAccountAsync(int accountId, int groupId);
    Task<BaseResult<GroupResource>> RemoveGroupFromAccountAsync(int accountId, int groupId);
    Task<BaseResult<IEnumerable<PersonResource>>> GetListPersonByGroupIdAsync(int groupId);
}