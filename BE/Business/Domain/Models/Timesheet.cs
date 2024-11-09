using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class Timesheet : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public float TotalWorkDay { get; set; }
    public float WorkDay { get; set; }
    public float Absent { get; set; }
    public float Holiday { get; set; }
    public float UnpaidLeave { get; set; }
    public float PaidLeave { get; set; }
    public string TimesheetJSON { get; set; }
    public DateOnly Date { get; set; }
    public string PersonId { get; set; }
    public Person Person { get; set; }
}