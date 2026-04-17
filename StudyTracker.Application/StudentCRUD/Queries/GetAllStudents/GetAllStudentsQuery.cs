using MediatR;
using StudyTracker.Application.DTOs;

namespace StudyTracker.Application.StudentCRUD.Queries.GetAllStudents;

public record GetAllStudentsQuery() : IRequest<IEnumerable<StudentDto>>;
