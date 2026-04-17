using MediatR;
using StudyTracker.Application.DTOs;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetAllStudySessions;

public record GetAllStudySessionsQuery() : IRequest<IEnumerable<StudySessionDto>>;
