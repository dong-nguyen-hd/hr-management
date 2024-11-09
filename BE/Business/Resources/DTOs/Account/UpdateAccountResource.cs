using System.ComponentModel.DataAnnotations;
using Business.Extensions.Validation;

namespace Business.Resources.DTOs.Account;

public class UpdateAccountResource
{
    [Required]
    [Password]
    public string Password { get; set; }

    [Required]
    [MaxLength(500)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(500)]
    public string Email { get; set; }

    [Required]
    [Role]
    public int Role { get; set; }

    [MaxLength(50)]
    public List<int> Group { get; set; }
}