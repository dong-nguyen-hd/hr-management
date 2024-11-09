using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.WorkHistory;

public class WorkHistoryResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(250)]
    public string Position { get; set; }

    [Required]
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