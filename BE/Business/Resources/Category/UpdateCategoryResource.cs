using Business.Resources.Technology;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Category;

public class UpdateCategoryResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public List<UpdateTechnologyResource> Technologies { get; set; }
}