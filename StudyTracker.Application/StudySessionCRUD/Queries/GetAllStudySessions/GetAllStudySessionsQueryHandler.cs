using AutoMapper;
using MediatR;
using StudyTracker.Application.DTOs;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetAllStudySessions;

public class GetAllStudySessionsQueryHandler : IRequestHandler<GetAllStudySessionsQuery, IEnumerable<StudySessionDto>>
{
    private readonly IStudySessionRepository _repository;
    private readonly IMapper _mapper;

    public GetAllStudySessionsQueryHandler(IStudySessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudySessionDto>> Handle(GetAllStudySessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<StudySessionDto>>(sessions);
    }
}
