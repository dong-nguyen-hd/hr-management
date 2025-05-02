using AutoMapper;
using Business.Communication;
using Business.Domain.Models;
using Business.Data;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.WorkHistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/work-history")]
public class WorkHistoryController : DongNguyenController<WorkHistoryResource, CreateWorkHistoryResource, UpdateWorkHistoryResource, WorkHistory>
{
    #region Constructor
    public WorkHistoryController(IWorkHistoryService workHistoryService,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(workHistoryService, mapper, responseMessage)
    {
        }
    #endregion

    #region Action
    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateWorkHistoryResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create a work-history.");

            return await base.CreateAsync(resource);
        }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateWorkHistoryResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update a work-history with Id is {id}.");

            return await base.UpdateAsync(id, resource);
        }

    [HttpPut]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> ChangeOrderIndexAsync([FromBody] List<int> ids)
    {
            Log.Information($"{User.Identity?.Name}: change order-index a work-history.");

            return await base.ChangeOrderIndexAsync(ids);
        }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<WorkHistoryResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete a work-history with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    #endregion
}