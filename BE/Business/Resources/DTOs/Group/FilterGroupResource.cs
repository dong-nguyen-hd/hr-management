using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Group;

public class FilterGroupResource
{
    [MaxLength(25)]
    public string Name { get; set; }

    public bool Available { get; set; }

    [Display(Name = "Last Day")]
    public DateTime? LastDay { get; set; }
}