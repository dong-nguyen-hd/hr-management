using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Department;
using API.Resources.Enums;
using API.Results;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/department")]
public class DepartmentController : DongNguyenController<DepartmentResource, CreateDepartmentResource, UpdateDepartmentResource, Department>
{
    #region Constructor
    public DepartmentController(IDepartmentService departmentService,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(departmentService, mapper, responseMessage)
    {
        }
    #endregion

    #region Action
    [HttpGet]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<DepartmentResource>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<DepartmentResource>>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<DepartmentResource>>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> GetAllAsync()
    {
            Log.Information($"{User.Identity?.Name}: get all department data.");

            return await base.GetAllAsync();
        }

    [HttpGet("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> GetByIdAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: get a department with Id is {id}.");

            return await base.GetByIdAsync(id);
        }

    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateDepartmentResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create a department.");

            return await base.CreateAsync(resource);
        }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDepartmentResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update a department with Id is {id}.");

            return await base.UpdateAsync(id, resource);
        }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<DepartmentResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete a department with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    #endregion
}