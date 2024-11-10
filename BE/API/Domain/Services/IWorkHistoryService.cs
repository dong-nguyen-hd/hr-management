using API.Resources.DTOs.WorkHistory.Request;
using API.Resources.DTOs.WorkHistory.Response;
using API.Results;

namespace API.Domain.Services;

public interface IWorkHistoryService : IBaseService
{
    Task<BaseResult<WorkHistoryResponse>> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<BaseResult<WorkHistoryResponse>> CreateAsync(CreateRequest request, CancellationToken cancellationToken = default);
    Task<BaseResult<WorkHistoryResponse>> UpdateAsync(string id, UpdateRequest request, CancellationToken cancellationToken = default);
    Task<DeleteResult<WorkHistoryResponse>> RemoveAsync(string id, CancellationToken cancellationToken = default);
    Task<DeleteResult<IEnumerable<WorkHistoryResponse>>> RemoveRangeAsync(List<string> ids, CancellationToken cancellationToken = default);
    Task<BaseResult<bool>> ChangeOrderIndexAsync(List<string> ids, CancellationToken cancellationToken = default);
}