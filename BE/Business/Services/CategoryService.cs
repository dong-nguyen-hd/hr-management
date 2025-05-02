using AutoMapper;
using Business.Communication;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Extensions;
using Business.Resources;
using Business.Resources.Category;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class CategoryService : BaseService<CategoryResource, CreateCategoryResource, UpdateCategoryResource, Category>, ICategoryService
{
    #region Property
    private readonly ICategoryRepository _categoryRepository;
    #endregion

    #region Constructor
    public CategoryService(ICategoryRepository categoryRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(categoryRepository, mapper, unitOfWork, responseMessage)
    {
        this._categoryRepository = categoryRepository;
    }
    #endregion

    #region Method
    public override async Task<BaseResponse<CategoryResource>> InsertAsync(CreateCategoryResource createCategoryResource)
    {
        try
        {
            // Validate category name is existent?
            var hasValue = await _categoryRepository.FindByNameAsync(createCategoryResource.Name, true);
            using (hasValue.GetEnumerator())
                if (hasValue.GetEnumerator().MoveNext())
                    return new BaseResponse<CategoryResource>(ResponseMessage.Values["Category_Existent"]);

            var tempCategory = Mapper.Map<CreateCategoryResource, Category>(createCategoryResource);

            await _categoryRepository.InsertAsync(tempCategory);
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<CategoryResource>(Mapper.Map<Category, CategoryResource>(tempCategory));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Category_Saving_Error"], ex);
        }
    }

    public override async Task<BaseResponse<CategoryResource>> UpdateAsync(int id, UpdateCategoryResource updateCategoryResource)
    {
        try
        {
            // Validate Id is existent?
            var tempCategory = await _categoryRepository.GetByIdAsync(id);
            if (tempCategory is null)
                return new BaseResponse<CategoryResource>(ResponseMessage.Values["Category_NoData"]);

            // Validate category name is existent?
            if (!tempCategory.Name.Equals(updateCategoryResource.Name))
            {
                var hasValue = await _categoryRepository.FindByNameAsync(updateCategoryResource.Name, true);
                if (hasValue.ToList().Count > 0)
                    return new BaseResponse<CategoryResource>(ResponseMessage.Values["Category_Existent"]);
            }

            // Update infomation
            Mapper.Map(updateCategoryResource, tempCategory);

            _categoryRepository.Update(tempCategory);
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<CategoryResource>(Mapper.Map<Category, CategoryResource>(tempCategory));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Category_Saving_Error"], ex);
        }
    }

    public async Task<PaginationResponse<IEnumerable<CategoryResource>>> GetPaginationAsync(QueryResource pagination, FilterCategoryResource filterResource)
    {
        var paginationCategory = await _categoryRepository.GetPaginationAsync(pagination, filterResource);

        // Mapping
        var tempResource = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(paginationCategory.records);

        var resource = new PaginationResponse<IEnumerable<CategoryResource>>(tempResource);

        // Using extension-method for pagination
        resource.CreatePaginationResponse(pagination, paginationCategory.total);

        return resource;
    }
    #endregion
}