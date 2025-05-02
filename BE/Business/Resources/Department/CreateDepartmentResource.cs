using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Department;

public class CreateDepartmentResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}