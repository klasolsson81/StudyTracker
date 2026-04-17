using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.StudentCRUD.Commands.CreateStudent;
using StudyTracker.Application.StudentCRUD.Commands.DeleteStudent;
using StudyTracker.Application.StudentCRUD.Commands.UpdateStudent;
using StudyTracker.Application.StudentCRUD.Queries.GetAllStudents;
using StudyTracker.Application.StudentCRUD.Queries.GetStudentById;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var students = await _mediator.Send(new GetAllStudentsQuery(), cancellationToken);
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var student = await _mediator.Send(new GetStudentByIdQuery(id), cancellationToken);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentCommand command, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentCommand command, CancellationToken cancellationToken)
    {
        // Säkerställer att id i URL och body matchar — förhindrar att klienten uppdaterar fel resurs.
        if (id != command.Id) return BadRequest("Id i URL matchar inte body.");

        var wasUpdated = await _mediator.Send(command, cancellationToken);
        return wasUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var wasDeleted = await _mediator.Send(new DeleteStudentCommand(id), cancellationToken);
        return wasDeleted ? NoContent() : NotFound();
    }
}
