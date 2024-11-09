using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class Category : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Name { get; set; } = null!;
    public HashSet<CategoryPerson> ListCategoryPerson { get; set; }
    public HashSet<Technology> Technologies { get; set; }
}