using MediatR;
using StudyTracker.Application.DTOs.Auth;

namespace StudyTracker.Application.Auth.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<AuthResponseDto?>;
