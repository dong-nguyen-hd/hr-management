using System.ComponentModel.DataAnnotations;
using Business.Extensions.Validation;

namespace Business.Resources.DTOs.Authentication;

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