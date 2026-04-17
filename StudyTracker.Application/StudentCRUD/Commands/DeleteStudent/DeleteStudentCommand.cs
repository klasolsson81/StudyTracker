using MediatR;

namespace StudyTracker.Application.StudentCRUD.Commands.DeleteStudent;

public record DeleteStudentCommand(int Id) : IRequest<bool>;
