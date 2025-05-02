namespace Business.Domain.Models;

public class Technology
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}