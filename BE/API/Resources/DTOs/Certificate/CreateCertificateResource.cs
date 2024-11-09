using System.ComponentModel.DataAnnotations;
using API.Extensions.Validation;

namespace API.Resources.DTOs.Certificate;

public class CreateCertificateResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Provider { get; set; }

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