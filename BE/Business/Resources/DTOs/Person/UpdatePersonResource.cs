using System.ComponentModel.DataAnnotations;
using Business.Extensions.Validation;
using Business.Resources.Enums;

namespace Business.Resources.DTOs.Person;

public class UpdatePersonResource
{
    [Required]
    [MaxLength(500)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(500)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [EmailAddress]
    [MaxLength(500)]
    public string Email { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Phone]
    public string Phone { get; set; }

    [Required]
    [DoB]
    [DataType(DataType.Date)]
    [Display(Name = "Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Gender]
    public Gender Gender { get; set; }

    [Required]
    [Display(Name = "Position Id")]
    public int PositionId { get; set; }

    [Required]
    [Display(Name = "Department Id")]
    public int DepartmentId { get; set; }

    [Display(Name = "Group Id")]
    public int? GroupId { get; set; }

    [MaxLength(50)]
    [ComponentOrder]
    [Display(Name = "Order Index")]
    public List<int> OrderIndex { get; set; }
}