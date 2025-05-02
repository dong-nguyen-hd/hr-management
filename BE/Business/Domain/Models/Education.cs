namespace Business.Domain.Models;

public class Education
{
    public int Id { get; set; }
    public string CollegeName { get; set; }
    public string Major { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int OrderIndex { get; set; }
    public bool IsDeleted { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
}