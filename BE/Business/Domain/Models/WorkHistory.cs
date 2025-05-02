namespace Business.Domain.Models;

public class WorkHistory
{
    public int Id { get; set; }
    public string Position { get; set; }
    public string CompanyName { get; set; }
    public int OrderIndex { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
}