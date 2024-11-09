using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Department;

public class UpdateDepartmentResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}