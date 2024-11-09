using System.ComponentModel.DataAnnotations;
using API.Extensions.Validation;
using API.Resources.DTOs.CategoryPerson;
using API.Resources.DTOs.Certificate;
using API.Resources.DTOs.Department;
using API.Resources.DTOs.Education;
using API.Resources.DTOs.Group;
using API.Resources.DTOs.Pay;
using API.Resources.DTOs.Position;
using API.Resources.DTOs.Project;
using API.Resources.DTOs.WorkHistory;
using API.Resources.Enums;

namespace API.Resources.DTOs.Person;

public class PersonResource
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(25)]
    [Display(Name = "Staff Id")]
    public string StaffId { get; set; }

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

    public AvatarResource Avatar { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Phone]
    [MaxLength(25)]
    public string Phone { get; set; }

    [Required]
    [DoB]
    [DataType(DataType.Date)]
    [JsonConverter(typeof(DateTimeConverter))]
    [Display(Name = "Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Gender]
    public Gender Gender { get; set; }

    [Required]
    [MaxLength(500)]
    [Display(Name = "Created By")]
    public string CreatedBy { get; set; }

    [Required]
    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    public bool Available { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Order Index")]
    public List<int> OrderIndex { get; set; }

    public PositionResource Position { get; set; }
    public PayResource Pay { get; set; }
    public DepartmentResource Department { get; set; }
    public GroupResource Group { get; set; }

    [Display(Name = "Work-History")]
    public List<WorkHistoryResource> WorkHistory { get; set; }

    [Display(Name = "Category-Person")]
    public List<CategoryPersonResource> CategoryPerson { get; set; } = new();
    public List<EducationResource> Education { get; set; }
    public List<CertificateResource> Certificate { get; set; }
    public List<ProjectResource> Project { get; set; } = new();
}