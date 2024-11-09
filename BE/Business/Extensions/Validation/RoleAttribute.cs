using System.ComponentModel.DataAnnotations;
using Business.Resources.Enums;

namespace Business.Extensions.Validation;

public class RoleAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
            try
            {
                if(value is null)
                    return ValidationResult.Success;

                if (Enum.IsDefined(typeof(eRole), value))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Invalid Role field.");
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid Role field.");
            }
        }
}