namespace API.Resources.DTOs.WorkHistory.Response;

public class WorkHistoryResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int OrderIndex { get; set; }
}