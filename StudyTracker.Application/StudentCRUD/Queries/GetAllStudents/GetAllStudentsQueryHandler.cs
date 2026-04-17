using MediatR;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudentCRUD.Queries.GetAllStudents;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>
{
    private readonly IStudentRepository _repository;

    public GetAllStudentsQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
