using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Position;

public class PositionResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}