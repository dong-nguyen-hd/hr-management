using AutoMapper;
using Business.Communication;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Extensions;
using Business.Data;
using Business.Resources;
using Business.Resources.Group;
using Business.Resources.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/group")]
public class GroupController : DongNguyenController<GroupResource, CreateGroupResource, UpdateGroupResource, Group>
{
    #region Constructor
    public GroupController(IGroupService groupService,
        IGroupRepository groupRepository,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(groupService, mapper, responseMessage)
    {
        this._groupRepository = groupRepository;
        this._groupService = groupService;
    }
    #endregion

    #region Property
    private readonly IGroupRepository _groupRepository;
    private readonly IGroupService _groupService;
    #endregion

    #region Action
    [HttpPost("pagination")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}")]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<GroupResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<GroupResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<GroupResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginationWithFilterAsync([FromQuery] int page, [FromQuery] int pageSize, [FromBody] FilterGroupResource filterResource)
    {
        Log.Information($"{User.Identity?.Name}: get pagination group.");

        QueryResource pagintation = new QueryResource(page, pageSize);

        var result = await _groupService.GetPaginationAsync(pagintation, filterResource);

        if (!result.Success)
            return BadRequest(result);

        if (result.Resource is null)
            return NoContent();

        return Ok(result);
    }

    [HttpGet("search")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}")]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<GroupResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<GroupResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<GroupResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FindAsync([FromQuery] string filterName)
    {
        Log.Information($"{User.Identity?.Name}: find group data with {filterName}-keyword.");

        var result = await _groupRepository.FindByNameAsync(filterName.RemoveSpaceCharacter());

        if (result is null)
            return NoContent();

        return Ok(new BaseResponse<IEnumerable<GroupResource>>(Mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(result)));
    }

    [HttpGet("list-person-view/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListPersonForViewAsync(int id)
    {
        Log.Information($"{User.Identity?.Name}: get list person with group-id is {id}.");

        var result = await _groupService.GetListPersonByGroupIdAsync(id);

        if (result is null)
            return NoContent();

        var mapObj = Mapper.Map<IEnumerable<PersonResource>, IEnumerable<PersonResourceView>>(result.Resource);

        return Ok(new BaseResponse<IEnumerable<PersonResourceView>>(mapObj));
    }

    [HttpGet("list-person-edit/{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListPersonForEditAsync(int id)
    {
        Log.Information($"{User.Identity?.Name}: get list person with group-id is {id}.");

        var result = await _groupService.GetListPersonByGroupIdAsync(id);

        if (result is null)
            return NoContent();

        return Ok(new BaseResponse<IEnumerable<PersonResource>>(result.Resource));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.Viewer}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> GetByIdAsync(int id)
    {
        Log.Information($"{User.Identity?.Name}: get a group with Id is {id}.");

        return await base.GetByIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateGroupResource resource)
    {
        Log.Information($"{User.Identity?.Name}: create a group.");

        return await base.CreateAsync(resource);
    }


    [HttpPost("add-account")]
    [Authorize(Roles = $"{Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGroupToAccountAsync([FromBody] GroupIdResource resource)
    {
        Log.Information($"{User.Identity?.Name}: add group-id '{resource.GroupId}' to account-id '{resource.AccountId}'.");

        var result = await _groupService.AddGroupToAccountAsync(resource.AccountId, resource.GroupId);

        if (!result.Success)
            return BadRequest(result);

        return StatusCode(201, result);
    }

    [HttpPost("remove-account")]
    [Authorize(Roles = $"{Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveGroupFromAccountAsync([FromBody] GroupIdResource resource)
    {
        Log.Information($"{User.Identity?.Name}: remove group-id '{resource.GroupId}' from account-id '{resource.AccountId}'.");

        var result = await _groupService.RemoveGroupFromAccountAsync(resource.AccountId, resource.GroupId);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateGroupResource resource)
    {
        Log.Information($"{User.Identity?.Name}: update a group with Id is {id}.");

        return await base.UpdateAsync(id, resource);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GroupResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
        Log.Information($"{User.Identity?.Name}: delete a group with Id is {id}.");

        return await base.DeleteAsync(id);
    }
    #endregion
}