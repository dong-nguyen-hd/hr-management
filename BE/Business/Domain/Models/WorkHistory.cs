using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class WorkHistory : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Position { get; set; } = null!;
    public string CompanyName { get; set; }  = null!;
    public int OrderIndex { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
    public string PersonId { get; set; }
    public Person Person { get; set; }
}