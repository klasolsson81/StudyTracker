using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.StudySessionCRUD.Commands.CreateStudySession;
using StudyTracker.Application.StudySessionCRUD.Commands.DeleteStudySession;
using StudyTracker.Application.StudySessionCRUD.Commands.UpdateStudySession;
using StudyTracker.Application.StudySessionCRUD.Queries.GetAllStudySessions;
using StudyTracker.Application.StudySessionCRUD.Queries.GetStudySessionById;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudySessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudySessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var sessions = await _mediator.Send(new GetAllStudySessionsQuery(), cancellationToken);
        return Ok(sessions);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var session = await _mediator.Send(new GetStudySessionByIdQuery(id), cancellationToken);
        return session is null ? NotFound() : Ok(session);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudySessionCommand command, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudySessionCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id) return BadRequest("Id i URL matchar inte body.");

        var wasUpdated = await _mediator.Send(command, cancellationToken);
        return wasUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var wasDeleted = await _mediator.Send(new DeleteStudySessionCommand(id), cancellationToken);
        return wasDeleted ? NoContent() : NotFound();
    }
}
