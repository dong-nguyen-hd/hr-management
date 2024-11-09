using Business.Resources.DTOs.Timesheet;
using Business.Results;

namespace Business.Domain.Services;

public interface ITimesheetService
{
    Task<BaseResult<TimesheetResource>> ImportAsync(Stream stream);
    Task<BaseResult<TimesheetResource>> GetTimesheetByPersonIdAsync(int personId, DateTime date);
}