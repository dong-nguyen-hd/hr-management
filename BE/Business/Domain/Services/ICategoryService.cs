using Business.Domain.Models;
using Business.Resources;
using Business.Resources.DTOs.Category;
using Business.Results;

namespace Business.Domain.Services;

public interface ICategoryService : IBaseService<CategoryResource, CreateCategoryResource, UpdateCategoryResource, Category>
{
    Task<PaginationResult<IEnumerable<CategoryResource>>> GetPaginationAsync(QueryResource pagination, FilterCategoryResource filterResource);
}