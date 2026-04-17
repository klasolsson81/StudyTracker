namespace StudyTracker.Application.DTOs;

public record StudentDto(
    int Id,
    string Name,
    string Email,
    string Class,
    IEnumerable<StudySessionDto> Sessions);
