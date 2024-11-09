using System.ComponentModel.DataAnnotations;
using API.Extensions.Validation;

namespace API.Resources.DTOs.Account;

public class UpdatePasswordAccountResource
{
    [Required]
    [Password]
    public string OldPassword { get; set; }

    [Required]
    [Password]
    public string NewPassword { get; set; }
}