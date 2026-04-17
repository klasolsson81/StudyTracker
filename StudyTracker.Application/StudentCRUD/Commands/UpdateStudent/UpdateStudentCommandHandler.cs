using MediatR;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudentCRUD.Commands.UpdateStudent;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
{
    private readonly IStudentRepository _repository;

    public UpdateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        // Hämtar befintlig student för att undvika att relationer skrivs över av detached update.
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) return false;

        existing.Name = request.Name;
        existing.Email = request.Email;
        existing.Class = request.Class;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }
}
