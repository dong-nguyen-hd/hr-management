using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class Education : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string CollegeName { get; set; } = null!;
    public string Major { get; set; }  = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int OrderIndex { get; set; }
    
    public string PersonId { get; set; }
    public Person Person { get; set; }
}