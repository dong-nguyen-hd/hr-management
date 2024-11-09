using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TechnologyRepository : BaseRepository<Technology>, ITechnologyRepository
{
    #region Constructor
    public TechnologyRepository(CoreContext context) : base(context) { }
    #endregion

    #region Method
    public async Task<IEnumerable<Technology>> FindByNameAsync(string filterName)
    {
        var queryable = Context.Technologies.AsQueryable();

        if (!string.IsNullOrEmpty(filterName))
            queryable = queryable.Where(x => x.Name.Contains(filterName));

        return await queryable.AsNoTracking().OrderBy(x => x.Name).Take(5).ToListAsync();
    }
    #endregion
}