using MediatR;

namespace StudyTracker.Application.StudentCRUD.Commands.UpdateStudent;

public record UpdateStudentCommand(int Id, string Name, string Email, string Class) : IRequest<bool>;
