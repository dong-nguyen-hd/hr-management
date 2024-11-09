using Business.Domain.Models.Base;

namespace Business.Domain.Models;

public class Category : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public HashSet<CategoryPerson> ListCategoryPerson { get; set; }
    public HashSet<Technology> Technologies { get; set; }
}