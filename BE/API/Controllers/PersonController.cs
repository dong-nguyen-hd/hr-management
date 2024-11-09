using API.Domain.Models;
using API.Domain.Services;
using API.Resources;
using API.Resources.DTOs.Person;
using API.Resources.Enums;
using API.Results;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/person")]
public class PersonController : DongNguyenController<PersonResource, CreatePersonResource, UpdatePersonResource, Person>
{
    #region Property
    private readonly IPersonService _personService;
    private readonly IImageService _imageService;
    #endregion

    #region Constructor
    public PersonController(IPersonService personService,
        IImageService imageService,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(personService, mapper, responseMessage)
    {
            this._personService = personService;
            this._imageService = imageService;
        }
    #endregion

    #region Action
    [HttpPut("image/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<>), 200)]
    [ProducesResponseType(typeof(BaseResult<>), 400)]
    public async Task<IActionResult> SaveImageAsync(int id, [FromForm] IFormFile image)
    {
            Log.Information($"{User.Identity?.Name}: save image a person with Id is {id}.");

            var filePath = Path.GetTempFileName();

            var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);
            stream.Position = 0;

            var result = await _imageService.SaveImagePersonAsync(id, stream);
            stream.Dispose();

            // Clean temp-file
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

    [HttpGet]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginationAsync([FromQuery] int page, [FromQuery] int pageSize)
    {
            Log.Information($"{User.Identity?.Name}: get pagination person.");

            QueryResource pagintation = new QueryResource(page, pageSize);

            var result = await _personService.GetPaginationAsync(pagintation, new FilterPersonResource());

            if (!result.Success)
                return BadRequest(result);

            if (result.Resource is null)
                return NoContent();

            return Ok(result);
        }

    [HttpPost("pagination")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginationWithFilterAsync([FromQuery] int page, [FromQuery] int pageSize, [FromBody] FilterPersonResource filterResource)
    {
            Log.Information($"{User.Identity?.Name}: get pagination person.");

            QueryResource pagintation = new QueryResource(page, pageSize);

            var result = await _personService.GetPaginationAsync(pagintation, filterResource);

            if (!result.Success)
                return BadRequest(result);

            if (result.Resource is null)
                return NoContent();

            return Ok(result);
        }

    [HttpPost("pagination-salary")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<PersonResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginationWithFilterAsync([FromQuery] int page, [FromQuery] int pageSize, [FromBody] FilterPersonSalaryResource filterResource)
    {
            Log.Information($"{User.Identity?.Name}: get pagination-salary person.");

            QueryResource pagintation = new QueryResource(page, pageSize);

            var result = await _personService.GetPaginationAsync(pagintation, filterResource);

            if (!result.Success)
                return BadRequest(result);

            if (result.Resource is null)
                return NoContent();

            return Ok(result);
        }

    [HttpGet("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> GetByIdAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: get a person with Id is {id}.");

            return await base.GetByIdAsync(id);
        }

    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}")]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreatePersonResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create a person.");

            resource.CreatedBy = User.Identity?.Name;

            var insertResult = await _personService.InsertAsync(resource);

            if (!insertResult.Success)
                return BadRequest(insertResult);

            return StatusCode(201, insertResult);
        }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePersonResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update a person with Id is {id}.");

            return await base.UpdateAsync(id, resource);
        }

    [HttpPut("reset-avatar/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}")]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetAvatarAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: reset avatar of the person with Id is {id}.");

            var result = await _imageService.ResetPersonAvatarAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<PersonResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete a person with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    #endregion
}