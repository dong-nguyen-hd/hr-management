using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Resources.DTOs.Timesheet;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TimesheetRepository : BaseRepository<Timesheet>, ITimesheetRepository
{
    #region Constructor

    public TimesheetRepository(CoreContext context) : base(context)
    {
    }

    #endregion

    #region Method

    public async Task<Timesheet> GetTimesheetByPersonIdAsync(int personId, DateTime date) =>
        await Context.Timesheets.AsNoTracking()
            .Where(x => x.PersonId == personId && x.Date.Year == date.Year && x.Date.Month == date.Month)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

    public async Task<WorkDayResource> GetTotalWorkDayAsync(int personId, DateTime date) =>
        await Context.Timesheets.AsNoTracking()
            .Where(x => x.PersonId == personId && x.Date.Year == date.Year && x.Date.Month == date.Month)
            .OrderByDescending(x => x.Id)
            .Select(x => new WorkDayResource() { WorkDay = x.WorkDay, TotalWorkDay = x.TotalWorkDay })
            .FirstOrDefaultAsync();

    #endregion
}