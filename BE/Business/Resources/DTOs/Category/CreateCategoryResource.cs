using System.ComponentModel.DataAnnotations;
using Business.Resources.DTOs.Technology;

namespace Business.Resources.DTOs.Category;

public class CreateCategoryResource
{
    [Required]
    [MaxLength(250)]
    [MinLength(1)]
    public string Name { get; set; }

    [MaxLength(50)]
    public List<CreateTechnologyResource> Technologies { get; set; }
}