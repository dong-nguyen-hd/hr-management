using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Timesheet;

public class TimesheetResource
{
    public int Id { get; set; }

    [Display(Name = "Total Work Day")]
    public float TotalWorkDay { get; set; }

    [Display(Name = "Work Day")]
    public float WorkDay { get; set; }
    public float? Absent { get; set; }
    public float? Holiday { get; set; }

    [Display(Name = "Unpaid Leave")]
    public float? UnpaidLeave { get; set; }

    [Display(Name = "Paid Leave")]
    public float? PaidLeave { get; set; }
    public string TimesheetJSON { get; set; }
    public DateTime Date { get; set; }
}