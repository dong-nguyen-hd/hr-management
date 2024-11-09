using System.ComponentModel.DataAnnotations;
using Business.Extensions.Validation;

namespace Business.Resources.DTOs.Account;

public class UpdatePasswordAccountResource
{
    [Required]
    [Password]
    public string OldPassword { get; set; }

    [Required]
    [Password]
    public string NewPassword { get; set; }
}