using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Project;
using API.Resources.Enums;
using API.Results;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/project")]
public class ProjectController : DongNguyenController<ProjectResource, CreateProjectResource, UpdateProjectResource, Project>
{
    #region Constructor
    public ProjectController(IProjectService projectService,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(projectService, mapper, responseMessage)
    {
        }
    #endregion

    #region Action
    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateProjectResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create a project.");

            return await base.CreateAsync(resource);
        }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateProjectResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update a project with Id is {id}.");

            return await base.UpdateAsync(id, resource);
        }

    [HttpPut]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> ChangeOrderIndexAsync([FromBody] List<int> ids)
    {
            Log.Information($"{User.Identity?.Name}: change order-index a project.");

            return await base.ChangeOrderIndexAsync(ids);
        }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<ProjectResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete a project with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    #endregion
}