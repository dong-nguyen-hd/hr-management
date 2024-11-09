using API.Domain.Models;
using API.Domain.Services;
using API.Extensions;
using API.Resources.DTOs.Technology;
using API.Resources.Enums;
using API.Results;
using AutoMapper;
using Business.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/technology")]
public class TechnologyController : DongNguyenController<TechnologyResource, CreateTechnologyResource, UpdateTechnologyResource, Technology>
{
    #region Constructor
    public TechnologyController(ITechnologyService technologyService,
        ITechnologyRepository technologyRepository,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(technologyService, mapper, responseMessage)
    {
        this._technologyRepository = technologyRepository;
    }
    #endregion

    #region Property
    private readonly ITechnologyRepository _technologyRepository;
    #endregion

    #region Action
    [HttpGet("search")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<TechnologyResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<TechnologyResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<TechnologyResource>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FindAsync([FromQuery] string filterName)
    {
        Log.Information($"{User.Identity?.Name}: find technology data with {filterName}-keyword.");

        var result = await _technologyRepository.FindByNameAsync(filterName.RemoveSpaceCharacter());

        if (result is null)
            return NoContent();

        return Ok(new BaseResult<IEnumerable<TechnologyResource>>(Mapper.Map<IEnumerable<Technology>, IEnumerable<TechnologyResource>>(result)));
    }

    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<TechnologyResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<TechnologyResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateTechnologyResource resource)
    {
        Log.Information($"{User.Identity?.Name}: create a technology.");

        return await base.CreateAsync(resource);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<TechnologyResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<TechnologyResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTechnologyResource resource)
    {
        Log.Information($"{User.Identity?.Name}: update a technology with Id is {id}.");

        return await base.UpdateAsync(id, resource);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<TechnologyResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<TechnologyResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
        Log.Information($"{User.Identity?.Name}: delete a technology with Id is {id}.");

        return await base.DeleteAsync(id);
    }
    #endregion
}