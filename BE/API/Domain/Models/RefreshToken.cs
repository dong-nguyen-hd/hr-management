using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class RefreshToken : BaseModel
{
    public string Token { get; set; } = null!;
    public DateTime ExpireDatetimeUtc { get; set; }
    public string UserAgent { get; set; } = null!;
    public bool IsUsed { get; set; }
    public string AccountId { get; set; }
    public Account Account { get; set; }
}