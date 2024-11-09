using API.Extensions;

namespace API.Domain.Models.Base;

public abstract class BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public bool Active { get; set; }
    public DateTime CreatedDatetimeUtc { get; set; }
    public DateTime UpdatedDatetimeUtc { get; set; }
}