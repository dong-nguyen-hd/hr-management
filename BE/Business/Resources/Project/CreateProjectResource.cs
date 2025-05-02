using Business.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Project;

public class CreateProjectResource
{
    [Required]
    [MaxLength(250)]
    public string Position { get; set; }

    [Required]
    [MaxLength(250)]
    public string Responsibilities { get; set; }

    [Required]
    [StartDate("EndDate")]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [Display(Name = "Person Id")]
    public int PersonId { get; set; }

    [Required]
    [Display(Name = "Group Id")]
    public int GroupId { get; set; }
}