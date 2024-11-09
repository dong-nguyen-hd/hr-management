using System.ComponentModel.DataAnnotations;
using API.Extensions.Validation;

namespace API.Resources.DTOs.Group;

public class CreateGroupResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Team Size")]
    public int TeamSize { get; set; }

    [Required]
    [StartDate("EndDate")]
    [DataType(DataType.Date)]

    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [MaxLength(50)]
    public List<int> Technologies { get; set; }
}