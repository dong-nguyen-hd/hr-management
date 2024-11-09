using Business.Domain.Models.Base;

namespace Business.Domain.Models;

public class Group : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Name { get; set; }
    public string Description { get; set; }
    public int TeamSize { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Technologies { get; set; }
    public bool IsDeleted { get; set; }
    public HashSet<Project> Projects { get; set; }
    public HashSet<Account> Accounts { get; set; }
}