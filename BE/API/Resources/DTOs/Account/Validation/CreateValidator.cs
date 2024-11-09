using System.Text.RegularExpressions;
using API.Resources.DTOs.Account.Request;
using API.Resources.SystemData;
using FluentValidation;

namespace API.Resources.DTOs.Account.Validation;


public class CreateValidator : AbstractValidator<CreateRequest>
{
    public CreateValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .Must(x => x?.Length <= 150);
        
        // Validate pwd must be MD5 format
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .Must(x => x?.Length >= 6 && Regex.IsMatch(x, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled));
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .Must(x => x?.Length <= 150)
            .When(x => !string.IsNullOrEmpty(x.Name));
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
        
        RuleFor(x => x.SystemRoles).Must(x => x.TrueForAll(MyPolicy.IsValid));
    }
}