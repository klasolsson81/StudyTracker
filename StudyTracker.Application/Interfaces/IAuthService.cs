using StudyTracker.Application.DTOs.Auth;

namespace StudyTracker.Application.Interfaces;

// Abstraktion för auth-operationer. Implementationen i Infrastructure-lagret
// hanterar ASP.NET Identity + JWT-generering så Application inte behöver bero på dem.
public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(string userName, string email, string password, CancellationToken cancellationToken);
    Task<AuthResponseDto?> LoginAsync(string userName, string password, CancellationToken cancellationToken);
}
