using MediatR;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetStudySessionById;

public class GetStudySessionByIdQueryHandler : IRequestHandler<GetStudySessionByIdQuery, StudySession?>
{
    private readonly IStudySessionRepository _repository;

    public GetStudySessionByIdQueryHandler(IStudySessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudySession?> Handle(GetStudySessionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id, cancellationToken);
    }
}
