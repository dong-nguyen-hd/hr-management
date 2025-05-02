using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Person;

public class FilterPersonResource
{
    [MaxLength(25)]
    [Display(Name = "Staff Id")]
    public string StaffId { get; set; }

    [MaxLength(500)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Position Id")]
    public int? PositionId { get; set; }

    [Display(Name = "Department Id")]
    public int? DepartmentId { get; set; }

    [Display(Name = "Last Day")]
    public DateTime? LastDay { get; set; }

    public bool Available { get; set; }

    [Display(Name = "Technology Id")]
    [MaxLength(3)]
    public List<int> TechnologyId { get; set; }
}