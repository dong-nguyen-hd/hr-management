using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Technology;
using API.Results;
using Microsoft.Extensions.Options;

namespace API.Services;

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
    public override async Task<BaseResult<TechnologyResource>> InsertAsync(CreateTechnologyResource createTechnologyResource)
    {
        try
        {
            var tempTechnology = Mapper.Map<CreateTechnologyResource, Technology>(createTechnologyResource);

            await _technologyRepository.InsertAsync(tempTechnology);
            await UnitOfWork.CompleteAsync();

            return new BaseResult<TechnologyResource>(Mapper.Map<Technology, TechnologyResource>(tempTechnology));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Technology_Saving_Error"], ex);
        }
    }
    #endregion
}