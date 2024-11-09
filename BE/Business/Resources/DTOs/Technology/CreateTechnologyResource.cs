using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Technology;

public class CreateTechnologyResource
{
    [Required]
    [MaxLength(250)]
    [MinLength(1)]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Category Id")]
    public int CategoryId { get; set; }
}