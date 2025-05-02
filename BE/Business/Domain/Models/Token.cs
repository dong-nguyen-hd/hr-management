namespace Business.Domain.Models;

public class Token
{
    public int Id { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireTime { get; set; }
    public string UserAgent { get; set; }
    public bool IsUsed { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
}