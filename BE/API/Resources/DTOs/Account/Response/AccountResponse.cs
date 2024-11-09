namespace API.Resources.DTOs.Account.Response;

public class AccountResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public AvatarResource Avatar { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Last Activity")]
    public DateTime LastActivity { get; set; }

    public List<GroupResource> Groups { get; set; }
}