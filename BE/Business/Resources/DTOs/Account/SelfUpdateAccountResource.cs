using System.ComponentModel.DataAnnotations;

namespace Business.Resources.DTOs.Account;

public class SelfUpdateAccountResource
{
    [Required]
    [MaxLength(500)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(500)]
    public string Email { get; set; }
}