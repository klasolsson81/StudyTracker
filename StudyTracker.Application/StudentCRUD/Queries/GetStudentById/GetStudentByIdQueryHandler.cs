using AutoMapper;
using MediatR;
using StudyTracker.Application.DTOs;
using StudyTracker.Application.Interfaces;

namespace StudyTracker.Application.StudentCRUD.Queries.GetStudentById;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public GetStudentByIdQueryHandler(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return student is null ? null : _mapper.Map<StudentDto>(student);
    }
}
