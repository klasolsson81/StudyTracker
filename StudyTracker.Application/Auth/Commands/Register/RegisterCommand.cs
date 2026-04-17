using MediatR;
using StudyTracker.Application.DTOs.Auth;

namespace StudyTracker.Application.Auth.Commands.Register;

public record RegisterCommand(string UserName, string Email, string Password) : IRequest<AuthResponseDto?>;
