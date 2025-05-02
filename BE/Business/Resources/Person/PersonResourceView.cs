using Business.Data;
using Business.Extensions;
using Business.Extensions.Validation;
using Business.Resources.CategoryPerson;
using Business.Resources.Certificate;
using Business.Resources.Department;
using Business.Resources.Education;
using Business.Resources.Position;
using Business.Resources.Project;
using Business.Resources.WorkHistory;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Person;

public class PersonResourceView
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

    public AvatarResource Avatar { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    [DoB]
    [DataType(DataType.Date)]
    [JsonConverter(typeof(DateTimeConverter))]
    [Display(Name = "Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Gender]
    public eGender Gender { get; set; }

    [Required]
    [MaxLength(500)]
    [Display(Name = "Created By")]
    public string CreatedBy { get; set; }

    [Required]
    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Order Index")]
    public List<int> OrderIndex { get; set; }

    public PositionResource Position { get; set; }
    public DepartmentResource Department { get; set; }

    [Display(Name = "Work-History")]
    public List<WorkHistoryResource> WorkHistory { get; set; }

    [Display(Name = "Category-Person")]
    public List<CategoryPersonResource> CategoryPerson { get; set; } = new();
    public List<EducationResource> Education { get; set; }
    public List<CertificateResource> Certificate { get; set; }
    public List<ProjectResource> Project { get; set; } = new();
}