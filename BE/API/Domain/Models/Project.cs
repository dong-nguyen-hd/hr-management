using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Project : BaseModel
{
    public string Position { get; set; } = null!;
    public string Responsibilities { get; set; }  = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int OrderIndex { get; set; }
    
    public string PersonId { get; set; }
    public Person Person { get; set; }
    public string GroupId { get; set; }
    public Group Group { get; set; }
}