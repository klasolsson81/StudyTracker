using MediatR;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudySessionCRUD.Commands.UpdateStudySession;

public class UpdateStudySessionCommandHandler : IRequestHandler<UpdateStudySessionCommand, bool>
{
    private readonly IStudySessionRepository _repository;

    public UpdateStudySessionCommandHandler(IStudySessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateStudySessionCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) return false;

        existing.Subject = request.Subject;
        existing.Date = request.Date;
        existing.DurationMinutes = request.DurationMinutes;
        existing.MotivationBefore = request.MotivationBefore;
        existing.MotivationAfter = request.MotivationAfter;
        existing.Notes = request.Notes;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }
}
