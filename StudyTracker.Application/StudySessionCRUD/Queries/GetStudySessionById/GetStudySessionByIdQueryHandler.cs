using AutoMapper;
using MediatR;
using StudyTracker.Application.DTOs;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetStudySessionById;

public class GetStudySessionByIdQueryHandler : IRequestHandler<GetStudySessionByIdQuery, StudySessionDto?>
{
    private readonly IStudySessionRepository _repository;
    private readonly IMapper _mapper;

    public GetStudySessionByIdQueryHandler(IStudySessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StudySessionDto?> Handle(GetStudySessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return session is null ? null : _mapper.Map<StudySessionDto>(session);
    }
}
