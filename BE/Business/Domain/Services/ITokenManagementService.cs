using Business.Resources.DTOs.Authentication;
using Business.Results;

namespace Business.Domain.Services;

public interface ITokenManagementService
{
    Task<BaseResult<AccessTokenResource>> GenerateTokensAsync(LoginResource loginResource, DateTime now, string userAgent);
    Task<BaseResult<TokenResource>> GenerateNewTokensAsync(RefreshTokenResource loginResource, DateTime now);
    Task<BaseResult<object>> LogoutAsync(LogoutResource logoutResource);
}