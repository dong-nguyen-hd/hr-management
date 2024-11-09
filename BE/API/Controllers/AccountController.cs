using AutoMapper;
using Business.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Claims;
using API.Domain.Models;
using API.Domain.Services;
using API.Extensions;
using API.Resources;
using API.Resources.DTOs.Account;
using API.Resources.Enums;
using API.Results;

namespace API.Controllers;

[Route("api/v1/account")]
public class AccountController : DongNguyenController<AccountResource, CreateAccountResource, UpdateAccountResource, Account>
{
    #region Property
    private readonly IAccountService _accountService;
    private readonly IAccountRepository _accountRepository;
    private readonly IImageService _imageService;
    #endregion

    #region Constructor
    public AccountController(IAccountService accountService,
        IAccountRepository accountRepository,
        IImageService imageService,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(accountService, mapper, responseMessage)
    {
            this._accountService = accountService;
            this._accountRepository = accountRepository;
            this._imageService = imageService;
        }
    #endregion

    #region Action
    [HttpPut("image/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), 200)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), 400)]
    public async Task<IActionResult> SaveImageAsync(int id, [FromForm] IFormFile image)
    {
            Log.Information($"{User.Identity?.Name}: save image for Id is {id}.");

            var identifier = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!identifier.Equals(id.ToString()))
                return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            var filePath = Path.GetTempFileName();

            var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);
            stream.Position = 0;

            var result = await _imageService.SaveImageAccountAsync(id, stream);
            stream.Dispose();

            // Clean temp-file
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

    [HttpGet("with-group/{id:int}")]
    [Authorize(Roles = $"{Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdIncludeGroupAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: get account and group data with account-Id is {id}.");

            var result = await _accountRepository.GetByIdIncludeGroupAsync(id);

            if (result is null)
                return NoContent();

            return Ok(new BaseResult<AccountResource>(Mapper.Map<Account, AccountResource>(result)));
        }

    [HttpPut("reset-avatar/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetAvatarAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: reset avatar of the account with Id is {id}.");

            var result = await _imageService.ResetAccountAvatarAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    [HttpGet]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListPaginationAsync([FromQuery] int page, [FromQuery] int pageSize)
    {
            Log.Information($"{User.Identity?.Name}: get account data.");

            QueryResource pagintation = new QueryResource(page, pageSize);

            var identifier = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Role).Value;
            eRole? currentRole = (eRole)ConvertStringToRole(identifier);
            var result = await _accountService.GetPaginationAsync(pagintation, null, currentRole);

            if (!result.Success)
                return BadRequest(result);

            if (result.Resource is null)
                return NoContent();

            return Ok(result);
        }

    [HttpPost("pagination")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AccountResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginationWithFilterAsync([FromQuery] int page, [FromQuery] int pageSize, [FromBody] FilterAccountResource filterResource)
    {
            Log.Information($"{User.Identity?.Name}: get pagination account.");

            QueryResource pagintation = new QueryResource(page, pageSize);

            var identifier = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Role).Value;
            eRole? currentRole = (eRole)ConvertStringToRole(identifier);
            var result = await _accountService.GetPaginationAsync(pagintation, filterResource, currentRole);

            if (!result.Success)
                return BadRequest(result);

            if (result.Resource is null)
                return NoContent();

            return Ok(result);
        }

    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateAccountResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create account is {resource.UserName}.");

            var result = await _accountService.InsertAsync(resource);

            if (!result.Success)
                return BadRequest(result);

            return StatusCode(201, result);
        }

    [HttpPost("qtda")]
    [Authorize(Roles = $"{Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAccountViewerAsync([FromBody] CreateAccountResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create account is {resource.UserName}.");

            resource.Role = (int)eRole.Viewer;

            var result = await _accountService.InsertAsync(resource);

            if (!result.Success)
                return BadRequest(result);

            return StatusCode(201, result);
        }

    [HttpGet("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> GetByIdAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: get account data with Id is {id}.");

            return await base.GetByIdAsync(id);
        }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateAccountResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update account with Id is {id}.");

            if (id == -1) return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            return await base.UpdateAsync(id, resource);
        }

    [HttpPut("qtda/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAccountViewerAsync(int id, [FromBody] UpdateAccountResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update account with Id is {id}.");

            if (id == -1) return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            resource.Role = (int)eRole.Viewer;

            return await base.UpdateAsync(id, resource);
        }

    [HttpPut("self-update/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SelfUpdateAsync(int id, [FromBody] SelfUpdateAccountResource resource)
    {
            Log.Information($"{User.Identity?.Name}: self-update account with Id is {id}.");

            var identifier = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!identifier.Equals(id.ToString()))
                return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            var result = await _accountService.SelfUpdateAsync(id, resource);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    [HttpPut("change-password/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePasswordAccountResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update account password with Id is {id}.");

            // Check if the id belongs to me
            var identifier = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!identifier.Equals(id.ToString()))
                return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            // Checking duplicate password
            if (resource.OldPassword.Equals(resource.NewPassword))
                return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            var result = await _accountService.UpdatePasswordAsync(id, resource);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete account with Id is {id}.");

            if (id == -1) // id -1 is admin account
                return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            return await base.DeleteAsync(id);
        }

    [HttpDelete("qtda/{id:int}")]
    [Authorize(Roles = $"{Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAccountViewerAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete account with Id is {id}.");

            var result = await _accountService.RemoveAccountViewerAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    [HttpPost("delete-range")]
    [Authorize(Roles = $"{Role.Admin}")]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AccountResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteRangeAsync(List<int> ids)
    {
            Log.Information($"{User.Identity?.Name}: delete range account with Ids is {ids}.");

            if (ids.Count <= 0)
                return BadRequest(new BaseResult<AccountResource>(false));

            if (ids.Contains(-1)) // id - 1 is admin account
                return BadRequest(new BaseResult<AccountResource>(ResponseMessage.Values["Account_Not_Permitted"]));

            return await base.DeleteRangeAsync(ids);
        }

    #region Private work
    private static eRole? ConvertStringToRole(string roleString)
    {
            foreach (eRole item in Enum.GetValues(typeof(eRole)))
            {
                if (roleString.Equals(item.ToDescriptionString()))
                    return item;
            }

            return null;
        }
    #endregion

    #endregion
}