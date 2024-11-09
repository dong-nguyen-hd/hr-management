using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Position;

public class CreatePositionResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}