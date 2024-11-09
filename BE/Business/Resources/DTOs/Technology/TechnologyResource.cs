using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Technology;

public class TechnologyResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}