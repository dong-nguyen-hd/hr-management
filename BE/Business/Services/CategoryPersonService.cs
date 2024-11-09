using AutoMapper;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.DTOs.CategoryPerson;
using Business.Resources.DTOs.Technology;
using Business.Results;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class CategoryPersonService : BaseService<CategoryPersonResource, CreateCategoryPersonResource, UpdateCategoryPersonResource, CategoryPerson>, ICategoryPersonService
{
    #region Property
    private readonly ICategoryPersonRepository _categoryPersonRepository;
    #endregion

    #region Constructor
    public CategoryPersonService(ICategoryPersonRepository categoryPersonRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(categoryPersonRepository, mapper, unitOfWork, responseMessage)
    {
        this._categoryPersonRepository = categoryPersonRepository;
    }
    #endregion

    #region Method
    public override async Task<BaseResult<CategoryPersonResource>> InsertAsync(CreateCategoryPersonResource createCategoryPersonResource)
    {
        try
        {
            // Validate Category-Id is existent?
            if (await _categoryPersonRepository.ValidateExistent(createCategoryPersonResource.PersonId, createCategoryPersonResource.CategoryId))
                return new BaseResult<CategoryPersonResource>(ResponseMessage.Values["CategoryPerson_Id_Invalid"]);

            var tempCategoryPerson = Mapper.Map<CreateCategoryPersonResource, CategoryPerson>(createCategoryPersonResource);
            tempCategoryPerson.OrderIndex = await _categoryPersonRepository.MaximumOrderIndexAsync(createCategoryPersonResource.PersonId);

            await _categoryPersonRepository.InsertAsync(tempCategoryPerson);
            await UnitOfWork.CompleteAsync();

            // Mapping response
            var categoryPerson = await _categoryPersonRepository.GetByIdAsync(tempCategoryPerson.Id);
            var technologies = categoryPerson.Category.Technologies.Where(x => createCategoryPersonResource.Technologies.Contains(x.Id));
            var categoryPersonResource = Mapper.Map<CategoryPerson, CategoryPersonResource>(categoryPerson);
            categoryPersonResource.Technologies = Mapper.Map<IEnumerable<Technology>, IEnumerable<TechnologyResource>>(technologies).ToList();

            return new BaseResult<CategoryPersonResource>(categoryPersonResource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["CategoryPerson_Saving_Error"], ex);
        }
    }

    public async override Task<BaseResult<CategoryPersonResource>> UpdateAsync(int id, UpdateCategoryPersonResource updateCategoryPersonResource)
    {
        try
        {
            // Validate Id is existent?
            var categoryPerson = await _categoryPersonRepository.GetByIdAsync(id);
            if (categoryPerson is null)
                return new BaseResult<CategoryPersonResource>(ResponseMessage.Values["CategoryPerson_NoData"]);
            // Validate Category-Id is existent?
            if(categoryPerson.CategoryId != updateCategoryPersonResource.CategoryId)
                if (await _categoryPersonRepository.ValidateExistent(categoryPerson.PersonId, updateCategoryPersonResource.CategoryId))
                    return new BaseResult<CategoryPersonResource>(ResponseMessage.Values["CategoryPerson_Id_Invalid"]);

            // Mapping
            Mapper.Map(updateCategoryPersonResource, categoryPerson);

            await UnitOfWork.CompleteAsync();

            // Mapping response
            categoryPerson = await _categoryPersonRepository.GetByIdAsync(id);
            var technologies = categoryPerson.Category.Technologies.Where(x => updateCategoryPersonResource.Technologies.Contains(x.Id));
            var categoryPersonResource = Mapper.Map<CategoryPerson, CategoryPersonResource>(categoryPerson);
            categoryPersonResource.Technologies = Mapper.Map<IEnumerable<Technology>, IEnumerable<TechnologyResource>>(technologies).ToList();

            return new BaseResult<CategoryPersonResource>(categoryPersonResource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["CategoryPerson_Saving_Error"], ex);
        }
    }
    #endregion
}