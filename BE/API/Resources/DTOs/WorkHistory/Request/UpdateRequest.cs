namespace API.Resources.DTOs.WorkHistory.Request;

public class UpdateRequest
{
    public string? CompanyName { get; set; }
    public string? Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}