using MediatR;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudySessionCRUD.Queries.GetStudySessionById;

public record GetStudySessionByIdQuery(int Id) : IRequest<StudySession?>;
