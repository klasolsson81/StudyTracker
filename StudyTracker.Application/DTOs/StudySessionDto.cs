namespace StudyTracker.Application.DTOs;

public record StudySessionDto(
    int Id,
    string Subject,
    DateTime Date,
    int DurationMinutes,
    int MotivationBefore,
    int MotivationAfter,
    string? Notes,
    int StudentId);
