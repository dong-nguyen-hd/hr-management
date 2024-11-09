using System.ComponentModel.DataAnnotations;
using API.Extensions.Validation;

namespace API.Resources.DTOs.Account.Request;

public class CreateRequest
{
    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    [Required]
    [Role]
    public int Role { get; set; }

    [MaxLength(50)]
    public List<string?> Group { get; set; }
}