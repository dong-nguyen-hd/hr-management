using System.ComponentModel.DataAnnotations;

namespace Business.Extensions.Validation;

/// <summary>
/// Provisions of Article 3 of the 2012 Vietnam Labor Code
/// </summary>
public class DoBAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
            try
            {
                DateTime date = Convert.ToDateTime(value);
                var max = DateTime.Now.AddYears(-15);
                var min = DateTime.Now.AddYears(-100);
                var msg = string.Format($"Please enter a value between {min:MM/dd/yyyy} and {max:MM/dd/yyyy}");
                if (date < min || date > max)
                    return new ValidationResult(msg);
                else
                    return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid Date of Birth");
            }
        }
}