using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Position;

public class CreatePositionResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}