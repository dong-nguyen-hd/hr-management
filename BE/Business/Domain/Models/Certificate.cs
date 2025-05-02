namespace Business.Domain.Models;

public class Certificate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Provider { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int OrderIndex { get; set; }
    public bool IsDeleted { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
}