namespace Business.Domain.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public HashSet<CategoryPerson> ListCategoryPerson { get; set; }
    public HashSet<Technology> Technologies { get; set; }
}