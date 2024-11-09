using Business.Domain.Models;
using Business.Resources;
using Business.Resources.DTOs.Person;
using Business.Results;

namespace Business.Domain.Services;

public interface IPersonService : IBaseService<PersonResource, CreatePersonResource, UpdatePersonResource, Person>
{
    Task<PaginationResult<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonResource filterResource);
    Task<PaginationResult<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonSalaryResource filterResource);
}