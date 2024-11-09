using System.ComponentModel.DataAnnotations;
using API.Resources.DTOs.Technology;

namespace API.Resources.DTOs.Category;

public class CategoryResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public List<TechnologyResource> Technologies { get; set; }
}