using API.Domain.Models.Base;
using API.Resources.Enums;

namespace API.Domain.Models;

public sealed class Person : BaseModel
{
    public string StaffId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public string? Description { get; set; }
    public string? Phone { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string OrderIndex { get; set; } = null!;

    public string PositionId { get; set; }
    public Position Position { get; set; }
    public string DepartmentId { get; set; }
    public Department Department { get; set; }
    public string? GroupId { get; set; }
    public Group Group { get; set; }
    public HashSet<Certificate> Certificates { get; set; }
    public HashSet<CategoryPerson> CategoryPersons { get; set; }
    public HashSet<Education> Educations { get; set; }
    public HashSet<Project> Projects { get; set; }
    public HashSet<WorkHistory> WorkHistories { get; set; }
    public HashSet<Pay> Pays { get; set; }
    public HashSet<Timesheet> Timesheets { get; set; }
}