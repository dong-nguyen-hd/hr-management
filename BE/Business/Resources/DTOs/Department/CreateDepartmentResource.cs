using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Department;

public class CreateDepartmentResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}