using MediatR;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudentCRUD.Queries.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<Student?>;
