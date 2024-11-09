using Business.Resources;
using Business.Results;

namespace Business.Domain.Services;

public interface IBaseService<Response, Insert, Update, Entity>
{
    Task<BaseResult<Response>> GetByIdAsync(int id);
    Task<BaseResult<IEnumerable<Response>>> GetAllAsync();
    Task<BaseResult<Response>> InsertAsync(Insert insertResource);
    Task<BaseResult<Response>> UpdateAsync(int id, Update updateResource);
    Task<BaseResult<Response>> RemoveAsync(int id);
    Task<DeleteResult<IEnumerable<Response>>> RemoveRangeAsync(List<int> ids);
    Task<BaseResult<Response>> ChangeOrderIndexAsync(List<int> ids);
}