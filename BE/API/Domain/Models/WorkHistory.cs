using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class WorkHistory : BaseModel
{
    public string Position { get; set; } = null!;
    public string CompanyName { get; set; }  = null!;
    public int OrderIndex { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
    public string PersonId { get; set; }
    public Person Person { get; set; }
}