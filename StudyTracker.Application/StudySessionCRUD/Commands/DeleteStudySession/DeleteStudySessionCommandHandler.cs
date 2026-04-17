using MediatR;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudySessionCRUD.Commands.DeleteStudySession;

public class DeleteStudySessionCommandHandler : IRequestHandler<DeleteStudySessionCommand, bool>
{
    private readonly IStudySessionRepository _repository;

    public DeleteStudySessionCommandHandler(IStudySessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteStudySessionCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
