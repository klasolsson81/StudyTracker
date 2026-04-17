using FluentValidation;
using StudyTracker.Application.Auth.Commands.Register;

namespace StudyTracker.Application.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Användarnamn är obligatoriskt.")
            .MinimumLength(3).WithMessage("Användarnamnet måste vara minst 3 tecken.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email är obligatorisk.")
            .EmailAddress().WithMessage("Email har ogiltigt format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Lösenord är obligatoriskt.")
            .MinimumLength(8).WithMessage("Lösenordet måste vara minst 8 tecken.");
    }
}
