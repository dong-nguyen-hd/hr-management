using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Position;

public class UpdatePositionResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}