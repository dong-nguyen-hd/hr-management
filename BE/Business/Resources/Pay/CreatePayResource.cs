using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Pay;

public class CreatePayResource
{
    [Range(0, double.MaxValue)]
    [Required]
    public decimal BaseSalary { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Allowance { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Bonus { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Display(Name = "Person Id")]
    public int PersonId { get; set; }
}