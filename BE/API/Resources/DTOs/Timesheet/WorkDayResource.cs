using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Timesheet;

public class WorkDayResource
{
    [Display(Name = "Total Work Day")]
    public float TotalWorkDay { get; set; }

    [Display(Name = "Work Day")]
    public float WorkDay { get; set; }
}