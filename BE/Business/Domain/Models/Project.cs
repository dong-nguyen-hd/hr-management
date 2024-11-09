using Business.Domain.Models.Base;

namespace Business.Domain.Models;

public class Project : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Position { get; set; }
    public string Responsibilities { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int OrderIndex { get; set; }
    public bool IsDeleted { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
}