namespace Business.Domain.Models;

public class Pay
{
    public int Id { get; set; }
    public float TotalWorkDay { get; set; }
    public float WorkDay { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Allowance { get; set; }
    public decimal Bonus { get; set; }
    public float PIT { get; set; }
    public float SocialInsurance { get; set; }
    public float HealthInsurance { get; set; }
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
}