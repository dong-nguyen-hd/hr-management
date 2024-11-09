using API.Resources;
using API.Resources.DTOs.Account;
using API.Resources.Enums;
using API.Results;

namespace API.Domain.Services;

public interface IAccountService : IBaseService
{
    Task<PaginationResult<IEnumerable<AccountResource>>> GetPaginationAsync(QueryResource pagintation, FilterAccountResource filterResource, eRole? role);
    Task<BaseResult<AccountResource>> SelfUpdateAsync(int id, SelfUpdateAccountResource resource);
    Task<BaseResult<AccountResource>> UpdatePasswordAsync(int id, UpdatePasswordAccountResource resource);
    Task<BaseResult<AccountResource>> RemoveAccountViewerAsync(int id);
    
    //
    Task<BaseResult<AccountResource>> GetByIdAsync(int id);
    Task<BaseResult<IEnumerable<AccountResource>>> GetAllAsync();
    Task<BaseResult<AccountResource>> InsertAsync(Insert insertResource);
    Task<BaseResult<AccountResource>> UpdateAsync(int id, Update updateResource);
    Task<BaseResult<AccountResource>> RemoveAsync(int id);
    Task<DeleteResult<IEnumerable<AccountResource>>> RemoveRangeAsync(List<int> ids);
    Task<BaseResult<AccountResource>> ChangeOrderIndexAsync(List<int> ids);
}