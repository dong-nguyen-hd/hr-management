using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class Project : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
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