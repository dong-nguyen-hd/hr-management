using System.ComponentModel.DataAnnotations;
using Business.Extensions.Validation;

namespace Business.Resources.DTOs.Account;

public class FilterAccountResource
{
    [MaxLength(125)]
    public string UserName { get; set; }

    [Role]
    public int? Role { get; set; }
}