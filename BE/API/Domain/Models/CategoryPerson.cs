using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class CategoryPerson : BaseModel
{
    public int OrderIndex { get; set; }
    public string Technologies { get; set; } = null!;
    public string CategoryId { get; set; }
    public string PersonId { get; set; }
    public Category Category { get; set; }
    public Person Person { get; set; }
}