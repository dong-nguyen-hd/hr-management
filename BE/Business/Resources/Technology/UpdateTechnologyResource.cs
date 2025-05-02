using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Technology;

public class UpdateTechnologyResource
{
    [Required]
    [MaxLength(250)]
    [MinLength(1)]
    public string Name { get; set; }
}