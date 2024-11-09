using System.ComponentModel.DataAnnotations;
using API.Resources.DTOs.Technology;

namespace API.Resources.DTOs.Category;

public class UpdateCategoryResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public List<UpdateTechnologyResource> Technologies { get; set; }
}