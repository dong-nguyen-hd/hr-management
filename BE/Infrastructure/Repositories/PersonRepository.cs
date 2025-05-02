using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Extensions;
using Business.Resources;
using Business.Resources.Person;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    #region Constructor
    public PersonRepository(AppDbContext context) : base(context) { }
    #endregion

    #region Method
    public async Task<(IEnumerable<Person> records, int total)> GetPaginationWithSalaryAsync(QueryResource pagination, FilterPersonSalaryResource filterResource)
    {
            DateTime now = filterResource.Date != null ? (DateTime)filterResource.Date : DateTime.UtcNow;

            var queryable = ConditionFilter(filterResource);

            var total = await queryable.CountAsync();

            var records = await queryable.AsNoTracking()
                .AsSplitQuery()
                .OrderByDescending(x => x.FirstName)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Include(y => y.Position)
                .Include(y => y.Department)
                .Include(y => y.Pays.Where(y => y.Date.Year == now.Year && y.Date.Month == now.Month))
                .ToListAsync();

            return (records, total);
        }

    public async Task<(IEnumerable<Person> records, int total)> GetPaginationAsync(QueryResource pagination, FilterPersonResource filterResource)
    {
            var queryable = ConditionFilter(filterResource);

            var total = await queryable.CountAsync();

            var records = await queryable.AsNoTracking()
                .AsSplitQuery()
                .OrderByDescending(x => x.StaffId)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Include(y => y.Position)
                .Include(y => y.Department)
                .Include(y => y.WorkHistories.OrderByDescending(z => z.OrderIndex))
                .Include(y => y.CategoryPersons.OrderByDescending(z => z.OrderIndex))
                .ThenInclude(z => z.Category)
                .Include(y => y.Educations.OrderByDescending(z => z.OrderIndex))
                .Include(y => y.Certificates.OrderByDescending(z => z.OrderIndex))
                .Include(y => y.Group)
                .Include(y => y.Projects.OrderByDescending(z => z.OrderIndex))
                .ThenInclude(z => z.Group)
                .ToListAsync();

            return (records, total);
        }

    public override async Task<Person> GetByIdAsync(int id) =>
        await Context.People
            .AsSplitQuery()
            .Include(y => y.Department)
            .Include(y => y.Position)
            .Include(y => y.WorkHistories.OrderByDescending(z => z.OrderIndex))
            .Include(y => y.CategoryPersons.OrderByDescending(z => z.OrderIndex))
            .ThenInclude(z => z.Category)
            .Include(y => y.Educations.OrderByDescending(z => z.OrderIndex))
            .Include(y => y.Certificates.OrderByDescending(z => z.OrderIndex))
            .Include(y => y.Group)
            .Include(y => y.Projects.OrderByDescending(z => z.OrderIndex))
            .ThenInclude(z => z.Group)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<int> TotalRecordAsync() =>
        await Context.People.CountAsync();

    #region Private work
    private IQueryable<Person> ConditionFilter(FilterPersonSalaryResource filterResource)
    {
            var queryable = Context.People.AsQueryable();

            if (filterResource != null)
            {
                if (!string.IsNullOrEmpty(filterResource.StaffId))
                    queryable = queryable.Where(x => x.StaffId.Contains(filterResource.StaffId.RemoveSpaceCharacter()));

                if (filterResource.PositionId != null)
                    queryable = queryable.Where(x => x.PositionId.Equals(filterResource.PositionId));

                if (filterResource.DepartmentId != null)
                    queryable = queryable.Where(x => x.DepartmentId.Equals(filterResource.DepartmentId));

                if (!string.IsNullOrEmpty(filterResource.FirstName))
                {
                    string fullName = filterResource.FirstName.RemoveSpaceCharacter().ToLower();
                    queryable = queryable.Where(x => x.FirstName.Contains(fullName));
                }
            }

            return queryable;
        }

    private IQueryable<Person> ConditionFilter(FilterPersonResource filterResource)
    {
            var queryable = Context.People.AsQueryable();

            if (filterResource != null)
            {
                if (!string.IsNullOrEmpty(filterResource.StaffId))
                    queryable = queryable.Where(x => x.StaffId.Contains(filterResource.StaffId.RemoveSpaceCharacter()));

                if (filterResource.LastDay != null)
                    queryable = queryable.Where(x => x.GroupId == null || (DateTime.Compare((DateTime)x.Group.EndDate, (DateTime)filterResource.LastDay) < 0));

                if (filterResource.PositionId != null)
                    queryable = queryable.Where(x => x.PositionId.Equals(filterResource.PositionId));

                if (filterResource.DepartmentId != null)
                    queryable = queryable.Where(x => x.DepartmentId.Equals(filterResource.DepartmentId));

                if (filterResource.Available)
                    queryable = queryable.Where(x => x.GroupId.Equals(null));

                if (!string.IsNullOrEmpty(filterResource.FirstName))
                {
                    string fullName = filterResource.FirstName.RemoveSpaceCharacter().ToLower();
                    queryable = queryable.Where(x => x.FirstName.Contains(fullName));
                }

                if (filterResource.TechnologyId?.Count > 0)
                {
                    var tempQueryable = Context.CategoryPersons.AsQueryable();

                    int count = filterResource.TechnologyId.Count;

                    // I only use 3 conditions, but you can customize conditions with more options
                    switch (count)
                    {
                        case 2:
                            {
                                var listPersonIdOne = tempQueryable.Where(x => x.Technologies.Contains(filterResource.TechnologyId[0].ToString())).Select(x => x.PersonId);

                                tempQueryable = tempQueryable.Where(x => listPersonIdOne.Contains(x.PersonId) &&
                                x.Technologies.Contains(filterResource.TechnologyId[1].ToString()));
                                break;
                            }
                        case 3:
                            {
                                var listPersonIdOne = tempQueryable.Where(x => x.Technologies.Contains(filterResource.TechnologyId[0].ToString())).Select(x => x.PersonId);

                                var listPersonIdTwo = tempQueryable.Where(x => listPersonIdOne.Contains(x.PersonId) &&
                                x.Technologies.Contains(filterResource.TechnologyId[1].ToString())).Select(x => x.PersonId);

                                tempQueryable = tempQueryable.Where(x => listPersonIdTwo.Contains(x.PersonId) &&
                                x.Technologies.Contains(filterResource.TechnologyId[2].ToString()));
                                break;
                            }
                        default:
                            tempQueryable = tempQueryable.Where(x => x.Technologies.Contains(filterResource.TechnologyId[0].ToString()));
                            break;
                    }

                    var listId = tempQueryable.Select(y => y.PersonId);

                    queryable = queryable.Where(x => listId.Contains(x.Id));
                    // Console.WriteLine($"SQl Query: {queryable.ToQueryString()}"); // Debug performance of sql query
                }
            }

            return queryable;
        }
    #endregion

    #endregion
}