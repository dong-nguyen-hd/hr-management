using System.ComponentModel.DataAnnotations;
using API.Resources.Enums;

namespace API.Extensions.Validation;

public class GenderAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
            try
            {
                if (Enum.IsDefined(typeof(Gender), value))
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