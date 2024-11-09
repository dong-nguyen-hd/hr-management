using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Authentication;

public class LogoutResource
{
    public int Id { get; set; }

    [Display(Name = "Refresh Token")]
    public string RefreshToken { get; set; }
}