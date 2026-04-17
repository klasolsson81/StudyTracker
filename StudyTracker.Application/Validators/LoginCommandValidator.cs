using FluentValidation;
using StudyTracker.Application.Auth.Commands.Login;

namespace StudyTracker.Application.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Användarnamn krävs.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Lösenord krävs.");
    }
}
