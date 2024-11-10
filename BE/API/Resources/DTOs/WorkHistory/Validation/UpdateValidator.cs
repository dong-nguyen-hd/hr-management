using API.Resources.DTOs.WorkHistory.Request;
using FluentValidation;

namespace API.Resources.DTOs.WorkHistory.Validation;

public class UpdateValidator : AbstractValidator<UpdateRequest>
{
    public UpdateValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .NotNull()
            .Must(x => x?.Length <= 250);

        RuleFor(x => x.Position)
            .NotEmpty()
            .NotNull()
            .Must(x => x?.Length <= 250);

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .NotNull()
            .Must(x => DateTime.Compare(DateTime.UtcNow.Date, x) <= 0);

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .NotNull()
            .Must((x, y) => DateTime.Compare(DateTime.UtcNow.Date, y.Value) <= 0 &&
                            DateTime.Compare(x.StartDate, y.Value) <= 0)
            .When(x => x.EndDate != null);
    }
}