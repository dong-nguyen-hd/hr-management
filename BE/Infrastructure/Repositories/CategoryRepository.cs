using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Extensions;
using Business.Resources;
using Business.Resources.Category;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    #region Constructor
    public CategoryRepository(AppDbContext context) : base(context) { }
    #endregion

    #region Method
    public override async Task<Category> GetByIdAsync(int id) =>
        await Context.Categories.Where(x => x.Id.Equals(id))
            .Include(x => x.Technologies)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Category>> FindByNameAsync(string filterName, bool absolute = false)
    {
            var queryable = Context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(filterName) && !absolute)
                queryable = queryable.Where(x => x.Name.Contains(filterName));

            if(!string.IsNullOrEmpty(filterName) && absolute)
                queryable = queryable.Where(x => x.Name.Equals(filterName));

            return await queryable
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.Name)
                .Take(5)
                .Include(x => x.Technologies)
                .ToListAsync();
        }

    public async Task<(IEnumerable<Category> records, int total)> GetPaginationAsync(QueryResource pagination, FilterCategoryResource filterResource)
    {
            var queryable = ConditionFilter(filterResource);

            var total = await queryable.CountAsync();

            var records = await queryable.AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.Name)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Include(y => y.Technologies)
                .ToListAsync();

            return (records, total);
        }

    private IQueryable<Category> ConditionFilter(FilterCategoryResource filterResource)
    {
            var queryable = Context.Categories.AsQueryable();

            if (filterResource != null)
            {
                if (!string.IsNullOrEmpty(filterResource.CategoryName))
                    queryable = queryable.Where(x => x.Name.Contains(filterResource.CategoryName.RemoveSpaceCharacter()));

                if (!string.IsNullOrEmpty(filterResource.TechnologyName))
                {
                    var categoryIds = Context.Technologies
                        .Where(x => x.Name.Contains(filterResource.TechnologyName.RemoveSpaceCharacter()))
                        .Select(y => y.CategoryId); ;

                    queryable = queryable.Where(x => categoryIds.Contains(x.Id));
                }
            }

            return queryable;
        }
    #endregion
}