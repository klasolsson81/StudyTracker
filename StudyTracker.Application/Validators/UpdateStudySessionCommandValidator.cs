using FluentValidation;
using StudyTracker.Application.StudySessionCRUD.Commands.UpdateStudySession;

namespace StudyTracker.Application.Validators;

public class UpdateStudySessionCommandValidator : AbstractValidator<UpdateStudySessionCommand>
{
    public UpdateStudySessionCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Subject).NotEmpty().MaximumLength(100);
        RuleFor(x => x.DurationMinutes).GreaterThan(0).LessThanOrEqualTo(1440);
        RuleFor(x => x.MotivationBefore).InclusiveBetween(1, 10);
        RuleFor(x => x.MotivationAfter).InclusiveBetween(1, 10);
        RuleFor(x => x.Notes).MaximumLength(1000);
    }
}
