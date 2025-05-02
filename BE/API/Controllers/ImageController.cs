using Business.Communication;
using Business.Domain.Services;
using Business.Resources.Information;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Business.Data;

namespace API.Controllers;

[ApiController]
[Route("api/v1/image")]
public class ImageController : ControllerBase
{
    #region Property
    private readonly IUriService _uriService;
    private readonly HostResource _hostResource;
    #endregion

    #region Constructor
    public ImageController(IUriService uriService,
        IOptionsMonitor<HostResource> hostResource)
    {
            this._uriService = uriService;
            this._hostResource = hostResource.CurrentValue;
        }
    #endregion

    #region Action
    [HttpGet("default-image")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status200OK)]
    public IActionResult GetDefaultUriAsync()
    {
            var path = new
            {
                OriginalImagePath = _uriService.GetRouteUri($"{_hostResource.OriginalImagePath}{Constant.DefaultAvatar}"),
                ThumbnailImagePath = _uriService.GetRouteUri($"{_hostResource.ThumbnailImagePath}{Constant.DefaultAvatar}")
            };

            return Ok(new BaseResponse<object>(path));
        }
    #endregion
}