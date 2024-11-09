using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Certificate : BaseModel
{
    public string Name { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int OrderIndex { get; set; }

    public string PersonId { get; set; }
    public Person Person { get; set; }
}