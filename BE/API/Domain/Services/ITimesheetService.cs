using API.Resources.DTOs.Timesheet;
using API.Results;

namespace API.Domain.Services;

public interface ITimesheetService
{
    Task<BaseResult<TimesheetResource>> ImportAsync(Stream stream);
    Task<BaseResult<TimesheetResource>> GetTimesheetByPersonIdAsync(int personId, DateTime date);
}