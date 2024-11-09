using Business.Domain.Services;
using Business.Resources;
using Business.Resources.DTOs.Authentication;
using Business.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("api/v1/token")]
public class TokenController : ControllerBase
{
    #region Property
    private readonly ITokenManagementService _tokenManagementService;
    protected readonly ResponseMessage ResponseMessage;
    #endregion

    #region Constructor
    public TokenController(ITokenManagementService tokenManagementService,
        IOptionsMonitor<ResponseMessage> responseMessage)
    {
            this._tokenManagementService = tokenManagementService;
            this.ResponseMessage = responseMessage.CurrentValue;
        }
    #endregion

    #region Action
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(BaseResult<AccessTokenResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccessTokenResource>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginResource resource)
    {
            string userAgent = Request.Headers["User-Agent"].ToString();
            var result = await _tokenManagementService.GenerateTokensAsync(resource, DateTime.UtcNow, userAgent);

            if (result.Success)
            {
                Log.Information($"{result.Resource.UserName}: is login.");
                return Ok(result);
            }

            return Unauthorized(result);
        }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(BaseResult<TokenResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<TokenResource>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GenerateNewTokensAsync([FromBody] RefreshTokenResource resource)
    {
            resource.UserAgent = Request.Headers["User-Agent"].ToString();
            var result = await _tokenManagementService.GenerateNewTokensAsync(resource, DateTime.UtcNow);

            Log.Information($"Account-id {resource.AccountId}: using refresh token with Id is {resource.Id} - {result.Success}.");

            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }

    [AllowAnonymous]
    [HttpPost("logout")]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LogoutAsync([FromBody] LogoutResource resource)
    {
            Log.Information($"Refresh-Token-Id {resource.Id}: is log out.");

            var result = await _tokenManagementService.LogoutAsync(resource);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    #endregion
}