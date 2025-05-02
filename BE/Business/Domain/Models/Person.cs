namespace Business.Domain.Models;

public class Person
{
    public int Id { get; set; }
    public string StaffId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public string Description { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public byte Gender { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string OrderIndex { get; set; }

    public int PositionId { get; set; }
    public Position Position { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int? GroupId { get; set; }
    public Group Group { get; set; }
    public HashSet<Certificate> Certificates { get; set; }
    public HashSet<CategoryPerson> CategoryPersons { get; set; }
    public HashSet<Education> Educations { get; set; }
    public HashSet<Project> Projects { get; set; }
    public HashSet<WorkHistory> WorkHistories { get; set; }
    public HashSet<Pay> Pays { get; set; }
    public HashSet<Timesheet> Timesheets { get; set; }
}