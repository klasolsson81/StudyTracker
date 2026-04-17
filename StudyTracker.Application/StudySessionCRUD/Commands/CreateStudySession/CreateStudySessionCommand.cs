using MediatR;
using StudyTracker.Application.DTOs;

namespace StudyTracker.Application.StudySessionCRUD.Commands.CreateStudySession;

public record CreateStudySessionCommand(
    int StudentId,
    string Subject,
    DateTime Date,
    int DurationMinutes,
    int MotivationBefore,
    int MotivationAfter,
    string? Notes) : IRequest<StudySessionDto>;
