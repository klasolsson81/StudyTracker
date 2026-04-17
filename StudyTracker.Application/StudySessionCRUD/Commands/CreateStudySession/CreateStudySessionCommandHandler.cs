using MediatR;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudySessionCRUD.Commands.CreateStudySession;

public class CreateStudySessionCommandHandler : IRequestHandler<CreateStudySessionCommand, StudySession>
{
    private readonly IStudySessionRepository _repository;

    public CreateStudySessionCommandHandler(IStudySessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudySession> Handle(CreateStudySessionCommand request, CancellationToken cancellationToken)
    {
        var session = new StudySession
        {
            StudentId = request.StudentId,
            Subject = request.Subject,
            Date = request.Date,
            DurationMinutes = request.DurationMinutes,
            MotivationBefore = request.MotivationBefore,
            MotivationAfter = request.MotivationAfter,
            Notes = request.Notes
        };

        return await _repository.AddAsync(session, cancellationToken);
    }
}
