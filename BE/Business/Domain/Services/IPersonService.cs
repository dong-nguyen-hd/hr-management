using Business.Communication;
using Business.Domain.Models;
using Business.Resources;
using Business.Resources.Person;

namespace Business.Domain.Services;

public interface IPersonService : IBaseService<PersonResource, CreatePersonResource, UpdatePersonResource, Person>
{
    Task<PaginationResponse<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonResource filterResource);
    Task<PaginationResponse<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonSalaryResource filterResource);
}