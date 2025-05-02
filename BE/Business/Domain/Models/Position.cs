namespace Business.Domain.Models;

public class Position
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public HashSet<Person> People { get; set; }
}