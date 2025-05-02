using Business.Data;
using System.ComponentModel.DataAnnotations;

namespace Business.Extensions.Validation;

public class GenderAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
            try
            {
                if (Enum.IsDefined(typeof(eGender), value))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Invalid Gender field.");
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid Gender field.");
            }
        }
}