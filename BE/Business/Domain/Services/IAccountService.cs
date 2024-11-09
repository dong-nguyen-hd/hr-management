using Business.Domain.Models;
using Business.Resources;
using Business.Resources.DTOs.Account;
using Business.Resources.Enums;
using Business.Results;

namespace Business.Domain.Services;

public interface IAccountService : IBaseService<AccountResource, CreateAccountResource, UpdateAccountResource, Account>
{
    Task<PaginationResult<IEnumerable<AccountResource>>> GetPaginationAsync(QueryResource pagintation, FilterAccountResource filterResource, eRole? role);
    Task<BaseResult<AccountResource>> SelfUpdateAsync(int id, SelfUpdateAccountResource resource);
    Task<BaseResult<AccountResource>> UpdatePasswordAsync(int id, UpdatePasswordAccountResource resource);
    Task<BaseResult<AccountResource>> RemoveAccountViewerAsync(int id);
}