using API.Resources.DTOs.Authentication;
using API.Results;

namespace API.Domain.Services;

public interface ITokenManagementService
{
    Task<BaseResult<AccessTokenResource>> GenerateTokensAsync(LoginResource loginResource, DateTime now, string userAgent);
    Task<BaseResult<TokenResource>> GenerateNewTokensAsync(RefreshTokenResource loginResource, DateTime now);
    Task<BaseResult<object>> LogoutAsync(LogoutResource logoutResource);
}