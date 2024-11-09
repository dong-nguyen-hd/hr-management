using API.Resources.SystemData;
using API.Results;

namespace API.Domain.Services;

public interface IBaseService
{
    BaseResult<Inner> GetBaseResult<Inner>(CodeMessage codeMessage, Inner? data = default, string message = "");
}