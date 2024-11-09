using API.Domain.Models;
using API.Resources.DTOs.CategoryPerson;

namespace API.Domain.Services;

public interface ICategoryPersonService : IBaseService<CategoryPersonResource, CreateCategoryPersonResource, UpdateCategoryPersonResource, CategoryPerson>
{
}