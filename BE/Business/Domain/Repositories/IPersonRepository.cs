using Business.Domain.Models;
using Business.Resources;
using Business.Resources.Person;

namespace Business.Domain.Repositories;

public interface IPersonRepository : IBaseRepository<Person>
{
    Task<(IEnumerable<Person> records, int total)> GetPaginationAsync(QueryResource pagination, FilterPersonResource filterResource);
    Task<(IEnumerable<Person> records, int total)> GetPaginationWithSalaryAsync(QueryResource pagination, FilterPersonSalaryResource filterResource);
    Task<int> TotalRecordAsync();
}