using Business.Communication;
using Business.Resources.Authentication;

namespace Business.Domain.Services;

public interface ITokenManagementService
{
    Task<BaseResponse<AccessTokenResource>> GenerateTokensAsync(LoginResource loginResource, DateTime now, string userAgent);
    Task<BaseResponse<TokenResource>> GenerateNewTokensAsync(RefreshTokenResource loginResource, DateTime now);
    Task<BaseResponse<object>> LogoutAsync(LogoutResource logoutResource);
}