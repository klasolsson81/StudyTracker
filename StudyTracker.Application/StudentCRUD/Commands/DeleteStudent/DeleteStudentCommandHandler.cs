using MediatR;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudentCRUD.Commands.DeleteStudent;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentRepository _repository;

    public DeleteStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
