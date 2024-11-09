using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Account : BaseModel
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    
    public List<string> SystemRoles { get; set; }
    public HashSet<RefreshToken> RefreshTokens { get; set; }
    public HashSet<Group> Groups { get; set; }
}