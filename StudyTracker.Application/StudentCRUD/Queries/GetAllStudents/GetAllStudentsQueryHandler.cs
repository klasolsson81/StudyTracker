using AutoMapper;
using MediatR;
using StudyTracker.Application.DTOs;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudentCRUD.Queries.GetAllStudents;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public GetAllStudentsQueryHandler(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }
}
