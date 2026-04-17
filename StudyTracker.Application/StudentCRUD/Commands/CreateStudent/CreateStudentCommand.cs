using MediatR;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudentCRUD.Commands.CreateStudent;

public record CreateStudentCommand(string Name, string Email, string Class) : IRequest<Student>;
