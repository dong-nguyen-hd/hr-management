using Business.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.WorkHistory;

public class CreateWorkHistoryResource
{
    [Required]
    [MaxLength(250)]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(250)]
    public string Position { get; set; }

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
}