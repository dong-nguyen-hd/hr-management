using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Technology;

public class UpdateTechnologyResource
{
    [Required]
    [MaxLength(250)]
    [MinLength(1)]
    public string Name { get; set; }
}