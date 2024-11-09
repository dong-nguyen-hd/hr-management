using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Category : BaseModel
{
    public string Name { get; set; } = null!;
    
    public HashSet<CategoryPerson> CategoriesPerson { get; set; }
    public HashSet<Technology> Technologies { get; set; }
}