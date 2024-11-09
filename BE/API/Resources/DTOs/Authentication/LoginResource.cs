using System.ComponentModel.DataAnnotations;
using API.Extensions.Validation;

namespace API.Resources.DTOs.Authentication;

public class LoginResource
{
    [Required]
    [MaxLength(125)]
    [UserName]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [Password]
    public string Password { get; set; }
}