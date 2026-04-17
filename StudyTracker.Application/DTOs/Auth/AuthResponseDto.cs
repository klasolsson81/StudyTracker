namespace StudyTracker.Application.DTOs.Auth;

public record AuthResponseDto(string Token, DateTime ExpiresAt, string UserName, IEnumerable<string> Roles);
