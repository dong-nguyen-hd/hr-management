using System.ComponentModel.DataAnnotations;
using API.Resources.DTOs.Technology;

namespace API.Resources.DTOs.Category;

public class CreateCategoryResource
{
    [Required]
    [MaxLength(250)]
    [MinLength(1)]
    public string Name { get; set; }

    [MaxLength(50)]
    public List<CreateTechnologyResource> Technologies { get; set; }
}