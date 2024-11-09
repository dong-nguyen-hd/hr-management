using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class RefreshToken : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Token { get; set; } = null!;
    public DateTime ExpireDatetimeUtc { get; set; }
    public string UserAgent { get; set; } = null!;
    public bool IsUsed { get; set; }
    public string AccountId { get; set; }
    public Account Account { get; set; }
}