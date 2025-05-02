using Business.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Account;

public class FilterAccountResource
{
    [MaxLength(125)]
    public string UserName { get; set; }

    [Role]
    public int? Role { get; set; }
}