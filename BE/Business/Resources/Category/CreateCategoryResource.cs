using Business.Resources.Technology;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Category;

public class CreateCategoryResource
{
    [Required]
    [MaxLength(250)]
    [MinLength(1)]
    public string Name { get; set; }

    [MaxLength(50)]
    public List<CreateTechnologyResource> Technologies { get; set; }
}