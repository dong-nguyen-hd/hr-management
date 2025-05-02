using Business.Communication;
using Business.Resources.Timesheet;

namespace Business.Domain.Services;

public interface ITimesheetService
{
    Task<BaseResponse<TimesheetResource>> ImportAsync(Stream stream);
    Task<BaseResponse<TimesheetResource>> GetTimesheetByPersonIdAsync(int personId, DateTime date);
}