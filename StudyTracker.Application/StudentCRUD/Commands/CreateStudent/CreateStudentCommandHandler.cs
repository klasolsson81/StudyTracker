using AutoMapper;
using MediatR;
using StudyTracker.Application.DTOs;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.StudentCRUD.Commands.CreateStudent;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public CreateStudentCommandHandler(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Class = request.Class
        };

        var created = await _repository.AddAsync(student, cancellationToken);
        return _mapper.Map<StudentDto>(created);
    }
}
