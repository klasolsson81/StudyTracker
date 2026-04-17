using MediatR;
using StudyTracker.Application.DTOs;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetStudySessionById;

public record GetStudySessionByIdQuery(int Id) : IRequest<StudySessionDto?>;
