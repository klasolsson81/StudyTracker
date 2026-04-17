using MediatR;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetAllStudySessions;

public class GetAllStudySessionsQueryHandler : IRequestHandler<GetAllStudySessionsQuery, IEnumerable<StudySession>>
{
    private readonly IStudySessionRepository _repository;

    public GetAllStudySessionsQueryHandler(IStudySessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StudySession>> Handle(GetAllStudySessionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
