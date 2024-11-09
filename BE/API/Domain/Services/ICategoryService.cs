using API.Domain.Models;
using API.Resources;
using API.Resources.DTOs.Category;
using API.Results;

namespace API.Domain.Services;

public interface ICategoryService : IBaseService<CategoryResource, CreateCategoryResource, UpdateCategoryResource, Category>
{
    Task<PaginationResult<IEnumerable<CategoryResource>>> GetPaginationAsync(QueryResource pagination, FilterCategoryResource filterResource);
}