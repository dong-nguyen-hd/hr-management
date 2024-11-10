using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.WorkHistory;
using Microsoft.Extensions.Options;

namespace API.Services;

public class WorkHistoryService(IMapper mapper, CoreContext context) : BaseService, IWorkHistoryService
{
    #region Method

    public async Task<BaseResult<MasterDataResponse>> GetMasterDataAsync(bool hasPopularity, CancellationToken cancellationToken = default)

    #endregion
}