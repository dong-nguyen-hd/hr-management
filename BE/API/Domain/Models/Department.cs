using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Department : BaseModel
{
    public string Name { get; set; } = null!;
    
    public HashSet<Person> People { get; set; }
}