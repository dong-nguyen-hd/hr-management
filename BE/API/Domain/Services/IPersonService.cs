using API.Domain.Models;
using API.Resources;
using API.Resources.DTOs.Person;
using API.Results;

namespace API.Domain.Services;

public interface IPersonService : IBaseService<PersonResource, CreatePersonResource, UpdatePersonResource, Person>
{
    Task<PaginationResult<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonResource filterResource);
    Task<PaginationResult<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonSalaryResource filterResource);
}