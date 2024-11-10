namespace API.Resources.DTOs.WorkHistory.Request;

public class CreateRequest
{
    public string? CompanyName { get; set; }
    public string? Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? PersonId { get; set; }
}