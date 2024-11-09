using API.Domain.Models.Base;

namespace API.Domain.Models;

public sealed class Pay : BaseModel
{
    public float TotalWorkDay { get; set; }
    public float WorkDay { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Bonus { get; set; }
    public float PIT { get; set; }
    public float SocialInsurance { get; set; }
    public float HealthInsurance { get; set; }
    public DateTime Date { get; set; }
    
    public string PersonId { get; set; }
    public Person Person { get; set; }
}