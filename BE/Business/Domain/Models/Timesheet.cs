namespace Business.Domain.Models;

public class Timesheet
{
    public int Id { get; set; }
    public float TotalWorkDay { get; set; }
    public float WorkDay { get; set; }
    public float Absent { get; set; }
    public float Holiday { get; set; }
    public float UnpaidLeave { get; set; }
    public float PaidLeave { get; set; }
    public string TimesheetJSON { get; set; }
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
}