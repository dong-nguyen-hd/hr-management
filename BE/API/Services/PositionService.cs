using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Position;
using API.Results;
using Microsoft.Extensions.Options;

namespace API.Services;

public class PositionService : BaseService<PositionResource, CreatePositionResource, UpdatePositionResource, Position>, IPositionService
{
    #region Property
    private readonly IPositionRepository _positionRepository;
    #endregion

    #region Constructor
    public PositionService(IPositionRepository positionRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(positionRepository, mapper, unitOfWork, responseMessage)
    {
        this._positionRepository = positionRepository;
    }
    #endregion

    #region Method
    public override async Task<BaseResult<PositionResource>> InsertAsync(CreatePositionResource createPositionResource)
    {
        try
        {
            // Validate position name is existent?
            var hasValue = await _positionRepository.FindByNameAsync(createPositionResource.Name, true);
            if (hasValue.Count > 0)
                return new BaseResult<PositionResource>(ResponseMessage.Values["Position_Existent"]);

            // Mapping Resource to Position
            var position = Mapper.Map<CreatePositionResource, Position>(createPositionResource);

            await _positionRepository.InsertAsync(position);
            await UnitOfWork.CompleteAsync();

            return new BaseResult<PositionResource>(Mapper.Map<Position, PositionResource>(position));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Position_Saving_Error"], ex);
        }
    }
    #endregion
}