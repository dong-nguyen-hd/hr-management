using System.ComponentModel.DataAnnotations;

namespace Business.Resources.Pay;

public class PayResource
{
    public int Id { get; set; }
    public decimal NET { get; set; }
    public decimal Gross { get; set; }

    [Display(Name = "Base Salary")]
    public decimal BaseSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Bonus { get; set; }

    [Display(Name = "Personal Income Tax")]
    public decimal PIT { get; set; }

    [Display(Name = "Personal Income Tax Percent")]
    public float PITPercent { get; set; }

    [Display(Name = "Social Insurance")]
    public decimal SocialInsurance { get; set; }

    [Display(Name = "Social Insurance Percent")]
    public float SocialInsurancePercent { get; set; }

    [Display(Name = "Health Insurance")]
    public decimal HealthInsurance { get; set; }

    [Display(Name = "Health Insurance Percent")]
    public float HealthInsurancePercent { get; set; }
    public DateTime Date { get; set; }
}