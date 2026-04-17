using FluentValidation;
using StudyTracker.Application.StudySessionCRUD.Commands.CreateStudySession;

namespace StudyTracker.Application.Validators;

public class CreateStudySessionCommandValidator : AbstractValidator<CreateStudySessionCommand>
{
    public CreateStudySessionCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("StudentId måste anges.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Ämne är obligatoriskt.")
            .MaximumLength(100);

        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("Längden måste vara minst 1 minut.")
            .LessThanOrEqualTo(1440).WithMessage("Längden får inte överstiga ett dygn (1440 min).");

        RuleFor(x => x.MotivationBefore)
            .InclusiveBetween(1, 10).WithMessage("MotivationBefore måste vara mellan 1 och 10.");

        RuleFor(x => x.MotivationAfter)
            .InclusiveBetween(1, 10).WithMessage("MotivationAfter måste vara mellan 1 och 10.");

        RuleFor(x => x.Notes).MaximumLength(1000);
    }
}
