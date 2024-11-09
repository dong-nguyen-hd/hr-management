using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class CategoryPerson : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public int OrderIndex { get; set; }
    public string Technologies { get; set; } = null!;
    public string CategoryId { get; set; }
    public string PersonId { get; set; }
    public Category Category { get; set; }
    public Person Person { get; set; }
}