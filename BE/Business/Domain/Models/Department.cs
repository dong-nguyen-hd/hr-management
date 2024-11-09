using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class Department : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Name { get; set; } = null!;
    
    public HashSet<Person> People { get; set; }
}