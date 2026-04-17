using MediatR;
using StudyTracker.Application.DTOs;

namespace StudyTracker.Application.StudentCRUD.Commands.CreateStudent;

public record CreateStudentCommand(string Name, string Email, string Class) : IRequest<StudentDto>;
