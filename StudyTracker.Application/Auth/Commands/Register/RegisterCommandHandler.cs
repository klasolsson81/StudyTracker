using MediatR;
using StudyTracker.Application.DTOs.Auth;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto?>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.UserName, request.Email, request.Password, cancellationToken);
    }
}
