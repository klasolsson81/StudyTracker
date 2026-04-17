using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.Auth.Commands.Login;
using StudyTracker.Application.Auth.Commands.Register;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return response is null
            ? BadRequest("Registreringen misslyckades. Kontrollera att användarnamn/email inte redan används.")
            : Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return response is null
            ? Unauthorized("Felaktigt användarnamn eller lösenord.")
            : Ok(response);
    }
}
