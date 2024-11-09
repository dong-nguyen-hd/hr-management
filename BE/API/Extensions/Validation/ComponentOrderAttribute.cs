using System.ComponentModel.DataAnnotations;
using API.Resources.Enums;

namespace API.Extensions.Validation;

public class ComponentOrderAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
            try
            {
                List<int> tempList = value as List<int>;

                foreach (var item in tempList)
                    if (!ValidateElement(item))
                        return new ValidationResult("Invalid Component field.");

                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid Component field.");
            }

            bool ValidateElement(int temp) => Enum.IsDefined(typeof(eOrder), temp);
        }
}