using Business.Data;
using Business.Extensions.Validation;
using Business.Resources.CategoryPerson;
using Business.Resources.Certificate;
using Business.Resources.Education;
using Business.Resources.Project;
using Business.Resources.WorkHistory;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Resources.Person;

public class CreatePersonResource
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
    [MaxLength(25)]
    public string Phone { get; set; }

    [Required]
    [DoB]
    [DataType(DataType.Date)]
    [Display(Name = "Date Of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Gender]
    public eGender Gender { get; set; }

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

    [JsonIgnore]
    [Display(Name = "Created By")]
    public string CreatedBy { get; set; }

    [Display(Name = "Category-Person Resource")]
    public List<CreateCategoryPersonResource> CategoryPersonResource { get; set; }

    [Display(Name = "Certificate Resource")]
    public List<CreateCertificateResource> CertificateResource { get; set; }

    [Display(Name = "Education Resource")]
    public List<CreateEducationResource> EducationResource { get; set; }

    [Display(Name = "Project Resource")]
    public List<CreateProjectResource> ProjectResource { get; set; }

    [Display(Name = "Work-History Resource")]
    public List<CreateWorkHistoryResource> WorkHistoryResource { get; set; }
}