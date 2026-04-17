using MediatR;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudentCRUD.Queries.GetAllStudents;

public record GetAllStudentsQuery() : IRequest<IEnumerable<Student>>;
