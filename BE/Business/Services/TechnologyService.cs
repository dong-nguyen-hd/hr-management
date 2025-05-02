using AutoMapper;
using Business.Communication;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.Technology;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class TechnologyService : BaseService<TechnologyResource, CreateTechnologyResource, UpdateTechnologyResource, Technology>, ITechnologyService
{
    #region Constructor
    public TechnologyService(ITechnologyRepository technologyRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(technologyRepository, mapper, unitOfWork, responseMessage)
    {
        this._technologyRepository = technologyRepository;
    }
    #endregion

    #region Property
    private readonly ITechnologyRepository _technologyRepository;
    #endregion

    #region Method
    public override async Task<BaseResponse<TechnologyResource>> InsertAsync(CreateTechnologyResource createTechnologyResource)
    {
        try
        {
            var tempTechnology = Mapper.Map<CreateTechnologyResource, Technology>(createTechnologyResource);

            await _technologyRepository.InsertAsync(tempTechnology);
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<TechnologyResource>(Mapper.Map<Technology, TechnologyResource>(tempTechnology));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Technology_Saving_Error"], ex);
        }
    }
    #endregion
}