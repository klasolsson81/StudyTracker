using MediatR;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetAllStudySessions;

public record GetAllStudySessionsQuery() : IRequest<IEnumerable<StudySession>>;
