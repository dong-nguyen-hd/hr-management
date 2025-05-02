using Business.Domain.Models;
using Business.Resources.Timesheet;

namespace Business.Domain.Repositories;

public interface ITimesheetRepository : IBaseRepository<Timesheet>
{
    Task<Timesheet> GetTimesheetByPersonIdAsync(int personId, DateTime date);

    Task<WorkDayResource> GetTotalWorkDayAsync(int personId, DateTime date);
}