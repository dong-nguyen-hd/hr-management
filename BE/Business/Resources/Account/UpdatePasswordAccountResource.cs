using Business.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Account;

public class UpdatePasswordAccountResource
{
    [Required]
    [Password]
    public string OldPassword { get; set; }

    [Required]
    [Password]
    public string NewPassword { get; set; }
}