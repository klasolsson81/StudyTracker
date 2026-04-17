using MediatR;

namespace StudyTracker.Application.StudySessionCRUD.Commands.UpdateStudySession;

public record UpdateStudySessionCommand(
    int Id,
    string Subject,
    DateTime Date,
    int DurationMinutes,
    int MotivationBefore,
    int MotivationAfter,
    string? Notes) : IRequest<bool>;
