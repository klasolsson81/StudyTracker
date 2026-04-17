using AutoMapper;
using MediatR;
using StudyTracker.Application.DTOs;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudySessionCRUD.Commands.CreateStudySession;

public class CreateStudySessionCommandHandler : IRequestHandler<CreateStudySessionCommand, StudySessionDto>
{
    private readonly IStudySessionRepository _repository;
    private readonly IMapper _mapper;

    public CreateStudySessionCommandHandler(IStudySessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StudySessionDto> Handle(CreateStudySessionCommand request, CancellationToken cancellationToken)
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

        var created = await _repository.AddAsync(session, cancellationToken);
        return _mapper.Map<StudySessionDto>(created);
    }
}
