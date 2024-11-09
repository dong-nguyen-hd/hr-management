using Business.Domain.Models.Base;

namespace Business.Domain.Models;

public class Position : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public HashSet<Person> People { get; set; }
}