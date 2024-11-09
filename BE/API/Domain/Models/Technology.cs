using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Technology : BaseModel
{
    public string Name { get; set; } = null!;
    public string CategoryId { get; set; }
    public Category Category { get; set; }
}