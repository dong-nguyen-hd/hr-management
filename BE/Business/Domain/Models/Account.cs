namespace Business.Domain.Models;

public class Account
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Avatar { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastActivity { get; set; }
    public bool IsDeleted { get; set; }
    public HashSet<Token> Tokens { get; set; }
    public HashSet<Group> Groups { get; set; }
}