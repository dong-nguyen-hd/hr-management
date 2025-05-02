using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Department;

public class UpdateDepartmentResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}