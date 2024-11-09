using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Group : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; } = null!;
    public int TeamSize { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Technologies { get; set; } = null!;
    
    public HashSet<Project> Projects { get; set; }
    public HashSet<Account> Accounts { get; set; }
}