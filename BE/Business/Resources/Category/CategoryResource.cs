using Business.Resources.Technology;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Category;

public class CategoryResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public List<TechnologyResource> Technologies { get; set; }
}