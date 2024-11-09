using AutoMapper;
using Business.Domain.Models;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.DTOs.Certificate;
using Business.Resources.Enums;
using Business.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[Route("api/v1/certificate")]
public class CertificateController : DongNguyenController<CertificateResource, CreateCertificateResource, UpdateCertificateResource, Certificate>
{
    #region Constructor
    public CertificateController(ICertificateService certificateService,
        IMapper mapper,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(certificateService, mapper, responseMessage)
    {
        }
    #endregion

    #region Action
    [HttpPost]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> CreateAsync([FromBody] CreateCertificateResource resource)
    {
            Log.Information($"{User.Identity?.Name}: create a certificate.");

            return await base.CreateAsync(resource);
        }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCertificateResource resource)
    {
            Log.Information($"{User.Identity?.Name}: update a certificate with Id is {id}.");

            return await base.UpdateAsync(id, resource);
        }

    [HttpPut]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> ChangeOrderIndexAsync([FromBody] List<int> ids)
    {
            Log.Information($"{User.Identity?.Name}: chnage order-index a certificate.");

            return await base.ChangeOrderIndexAsync(ids);
        }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorQTDA}")]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<CertificateResource>), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> DeleteAsync(int id)
    {
            Log.Information($"{User.Identity?.Name}: delete a certificate with Id is {id}.");

            return await base.DeleteAsync(id);
        }
    #endregion
}