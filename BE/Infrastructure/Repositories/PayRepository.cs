using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PayRepository : BaseRepository<Pay>, IPayRepository
{
    #region Constructor
    public PayRepository(AppDbContext context) : base(context) { }
    #endregion

    #region Method
    public async Task<Pay> GetByPersonIdAsync(int personId, DateTime date) =>
        await Context.Pays.AsNoTracking()
            .Where(x => x.PersonId == personId && x.Date.Year == date.Year && x.Date.Month == date.Month)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();
    #endregion
}