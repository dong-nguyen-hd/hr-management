using System.ComponentModel.DataAnnotations;
using Business.Extensions;
using Business.Extensions.Validation;
using Newtonsoft.Json;

namespace Business.Resources.DTOs.Certificate;

public class CertificateResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Provider { get; set; }

    [Required]
    [StartDate("EndDate")]
    [JsonConverter(typeof(DateTimeConverter))]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [Display(Name = "Order Index")]
    public int OrderIndex { get; set; }
}