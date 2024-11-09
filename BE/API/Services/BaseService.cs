using API.Domain.Services;
using API.Resources.SystemData;
using API.Results;

namespace API.Services;

public abstract class BaseService : IBaseService
{
    #region Method

    public virtual BaseResult<Inner> GetBaseResult<Inner>(CodeMessage codeMessage, Inner? data = default, string message = "") =>
        new()
        {
            Data = data,
            CodeMessage = codeMessage,
            Message = message
        };

    #endregion
}