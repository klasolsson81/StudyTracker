using FluentValidation;
using StudyTracker.Application.StudentCRUD.Commands.CreateStudent;

namespace StudyTracker.Application.Validators;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Namn är obligatoriskt.")
            .MaximumLength(100).WithMessage("Namn får vara max 100 tecken.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email är obligatorisk.")
            .EmailAddress().WithMessage("Email har ogiltigt format.")
            .MaximumLength(200).WithMessage("Email får vara max 200 tecken.");

        RuleFor(x => x.Class)
            .NotEmpty().WithMessage("Klass är obligatorisk.")
            .MaximumLength(50).WithMessage("Klass får vara max 50 tecken.");
    }
}
