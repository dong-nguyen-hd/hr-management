using Business.Domain.Models.Base;

namespace Business.Domain.Models;

public class CategoryPerson : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public int OrderIndex { get; set; }
    public string Technologies { get; set; }
    public bool IsDeleted { get; set; }
    public int CategoryId { get; set; }
    public int PersonId { get; set; }
    public Category Category { get; set; }
    public Person Person { get; set; }
}