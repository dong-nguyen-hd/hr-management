using Business.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Group;

public class UpdateGroupResource
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