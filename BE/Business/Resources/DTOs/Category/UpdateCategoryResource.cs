using System.ComponentModel.DataAnnotations;
using Business.Resources.DTOs.Technology;

namespace Business.Resources.DTOs.Category;

public class UpdateCategoryResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public List<UpdateTechnologyResource> Technologies { get; set; }
}