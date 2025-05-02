using System.ComponentModel.DataAnnotations;

namespace Business.Extensions.Validation;

public class StartDateAttribute : ValidationAttribute
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    private readonly string _tempEndDate;
    public StartDateAttribute(string endDate)
    {
            _tempEndDate = endDate;
        }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
            try
            {
                StartDate = Convert.ToDateTime(value);
                EndDate = GetValueEndDate(validationContext);

                if (default(DateTime) == StartDate)
                    new ValidationResult("Invalid Start Date/End Date field.");

                if (EndDate is null)
                {
                    if (DateTime.Compare(StartDate, DateTime.Now) > 0)
                        return new ValidationResult("Invalid Start Date/End Date field.");
                }
                else
                {
                    if (StartDate.Year > DateTime.Now.Year)
                        return new ValidationResult("Invalid Start Date/End Date field.");
                    if (DateTime.Compare(StartDate, Convert.ToDateTime(EndDate)) >= 0)
                        return new ValidationResult("Invalid Start Date/End Date field.");
                }
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid Start Date/End Date field.");
            }
        }

    protected DateTime? GetValueEndDate(ValidationContext validationContext)
    {
            try
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(_tempEndDate);

                if (propertyInfo != null)
                {
                    var tempEndDate = (DateTime?)propertyInfo.GetValue(validationContext.ObjectInstance);

                    return tempEndDate;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
}