using Business.Data;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Extensions;
using Business.Resources;
using Business.Resources.Account;
using Business.Resources.Authentication;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    #region Constructor
    public AccountRepository(AppDbContext context) : base(context) { }
    #endregion

    #region Method
    public async Task<Account> GetByIdIncludeGroupAsync(int accountId, bool isTracking = false)
    {
            var queryable = Context.Accounts.Where(x => x.Id.Equals(accountId));

            if (!isTracking) queryable = queryable.AsNoTracking();

            return await queryable.AsSplitQuery().Include(x => x.Groups).SingleOrDefaultAsync();
        }

    public async Task<Account> GetByIdAsync(int id, bool hasToken)
    {
            var queryable = Context.Accounts.Where(x => x.Id.Equals(id));

            if (hasToken) queryable.Include(x => x.Tokens);

            return await queryable.SingleOrDefaultAsync();
        }

    public async Task<(IEnumerable<Account> records, int total)> GetPaginationAsync(QueryResource pagination, FilterAccountResource filterResource, eRole? role)
    {
            var queryable = ConditionFilter(filterResource, role);

            var total = await queryable.CountAsync();

            var records = await queryable.AsNoTracking()
                .OrderBy(x => x.UserName)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return (records, total);
        }

    private IQueryable<Account> ConditionFilter(FilterAccountResource filterResource, eRole? role)
    {
            var queryable = Context.Accounts.AsQueryable();

            if (filterResource != null)
            {
                if (!string.IsNullOrEmpty(filterResource.UserName))
                    queryable = queryable.Where(x => x.UserName.Contains(filterResource.UserName.RemoveSpaceCharacter()));

                if (filterResource.Role != null)
                {
                    eRole filterRole = (eRole)filterResource.Role;
                    queryable = queryable.Where(x => x.Role.Equals(filterRole.ToDescriptionString()));
                }
            }

            if (role != null)
            {
                if (role == eRole.EditorQTDA)
                    queryable = queryable.Where(x => x.Role.Equals(Role.Viewer));
            }

            return queryable;
        }

    public async Task<int> TotalRecordAsync() => await Context.Accounts.CountAsync();

    public async Task<Account> ValidateCredentialsAsync(LoginResource loginResource)
    {
            var accountStored = await Context.Accounts
                .Where(x => x.UserName == loginResource.UserName.ToLower())
                .SingleOrDefaultAsync();

            if (accountStored is null)
                return null;
            else
            {
                // Validate credential
                bool isValid = accountStored.Password.CheckingPassword(loginResource.Password);
                if (isValid)
                    return accountStored;
                else
                    return null;
            }
        }

    /// <summary>
    /// Is user-name contain in database
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>
    /// <list type="bullet">
    /// <item>True: valid</item>
    /// <item>False: invalid</item>
    /// </list>
    /// </returns>
    public async Task<bool> ValidateUserNameAsync(string userName) =>
        !await Context.Accounts.Where(x => x.UserName == userName).AnyAsync();
    #endregion
}