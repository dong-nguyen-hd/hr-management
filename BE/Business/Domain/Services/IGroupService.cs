using Business.Communication;
using Business.Domain.Models;
using Business.Resources;
using Business.Resources.Group;
using Business.Resources.Person;

namespace Business.Domain.Services;

public interface IGroupService : IBaseService<GroupResource, CreateGroupResource, UpdateGroupResource, Group>
{
    Task<PaginationResponse<IEnumerable<GroupResource>>> GetPaginationAsync(QueryResource pagination, FilterGroupResource filterResource);
    Task<BaseResponse<GroupResource>> AddGroupToAccountAsync(int accountId, int groupId);
    Task<BaseResponse<GroupResource>> RemoveGroupFromAccountAsync(int accountId, int groupId);
    Task<BaseResponse<IEnumerable<PersonResource>>> GetListPersonByGroupIdAsync(int groupId);
}