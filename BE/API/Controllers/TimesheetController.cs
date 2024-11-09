using Business.Domain.Services;
using Business.Resources;
using Business.Resources.DTOs.Timesheet;
using Business.Resources.Enums;
using Business.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("api/v1/timesheet")]
public class TimesheetController : ControllerBase
{
    #region Property
    private readonly ITimesheetService _timesheetService;
    protected readonly ResponseMessage ResponseMessage;
    #endregion

    #region Constructor
    public TimesheetController(ITimesheetService timesheetService,
        IOptionsMonitor<ResponseMessage> responseMessage)
    {
            this._timesheetService = timesheetService;
            this.ResponseMessage = responseMessage.CurrentValue;
        }
    #endregion

    #region Action
    [HttpPost("import")]
    //[Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseResult<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportAsync([FromForm] IFormFile file)
    {
            // Validate file import
            var validateResult = ValidateTimesheet(file);
            if (!validateResult.isSuccess) return BadRequest(validateResult.result);

            var filePath = Path.GetTempFileName();

            var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var result = await _timesheetService.ImportAsync(stream);
            stream.Dispose();

            // Clean temp-file
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

    [HttpGet()]
    [Authorize(Roles = $"{Role.Admin}, {Role.EditorQTNS}, {Role.EditorKT}")]
    [ProducesResponseType(typeof(BaseResult<TimesheetResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<TimesheetResource>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTimesheetByPersonIdAsync(int personId, DateTime date)
    {
            Log.Information($"{User.Identity?.Name}: get a timesheet by person-id-{personId} data.");

            var result = await _timesheetService.GetTimesheetByPersonIdAsync(personId, date);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    #endregion

    #region Private work
    private (bool isSuccess, BaseResult<object> result) ValidateTimesheet(IFormFile file)
    {
            if (file == null || file.Length <= 0)
                return (false, new BaseResult<object>(ResponseMessage.Values["File_Empty"]));

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return (false, new BaseResult<object>(ResponseMessage.Values["Not_Support_File"]));

            return (true, new BaseResult<object>(true));
        }
    #endregion
}