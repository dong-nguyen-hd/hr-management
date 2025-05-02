using AutoMapper;
using Business.Communication;
using Business.Data;
using Business.Domain.Models;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.Education;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/education")]
public class EducationController : DongNguyenController<EducationResource, CreateEducationResource, UpdateEducationResource, Education>
{
    #region Constructor
    public EducationController(IEducationService educationService,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(educationService, mapper, responseMessage)
    {
        }
    #endregion

    #region Action
    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateEducationResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create a education.");

            return await base.CreateAsync(resource);
        }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateEducationResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update a education with Id is {id}.");

            return await base.UpdateAsync(id, resource);
        }

    [HttpPut]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> ChangeOrderIndexAsync([FromBody] List<int> ids)
    {
            Log.Information($"{User.Identity?.Name}: change order-index a education.");

            return await base.ChangeOrderIndexAsync(ids);
        }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<EducationResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete a education with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    #endregion
}