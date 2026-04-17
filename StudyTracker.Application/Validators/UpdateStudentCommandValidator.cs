using FluentValidation;
using StudyTracker.Application.StudentCRUD.Commands.UpdateStudent;

namespace StudyTracker.Application.Validators;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id måste vara större än 0.");
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Class).NotEmpty().MaximumLength(50);
    }
}
