using MediatR;
using StudyTracker.Application.DTOs.Auth;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto?>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request.UserName, request.Password, cancellationToken);
    }
}
