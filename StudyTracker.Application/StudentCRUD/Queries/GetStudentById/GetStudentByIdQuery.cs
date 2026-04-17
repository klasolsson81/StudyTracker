using MediatR;
using StudyTracker.Application.DTOs;

namespace StudyTracker.Application.StudentCRUD.Queries.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<StudentDto?>;
