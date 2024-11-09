using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Timesheet : BaseModel
{
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