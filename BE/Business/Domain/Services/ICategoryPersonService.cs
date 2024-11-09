using Business.Domain.Models;
using Business.Resources.DTOs.CategoryPerson;

namespace Business.Domain.Services;

public interface ICategoryPersonService : IBaseService<CategoryPersonResource, CreateCategoryPersonResource, UpdateCategoryPersonResource, CategoryPerson>
{
}