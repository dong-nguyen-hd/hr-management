using System.ComponentModel.DataAnnotations;

namespace API.Resources.DTOs.Pay;

public class UpdatePayResource
{
    [Range(0, double.MaxValue)]
    public decimal BaseSalary { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Allowance { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Bonus { get; set; }
}