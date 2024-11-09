using System.ComponentModel.DataAnnotations;
using API.Extensions.Validation;

namespace API.Resources.DTOs.Account;

public class FilterAccountResource
{
    [MaxLength(125)]
    public string UserName { get; set; }

    [Role]
    public int? Role { get; set; }
}