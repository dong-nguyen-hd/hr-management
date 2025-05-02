using Business.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Authentication;

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