namespace Business.Domain.Models.Base;

public abstract class BaseModel
{
    public bool Active { get; set; }
    public DateTime CreatedDatetimeUtc { get; set; }
    public DateTime UpdatedDatetimeUtc { get; set; }
}