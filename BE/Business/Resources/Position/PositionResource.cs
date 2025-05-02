using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Position;

public class PositionResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}