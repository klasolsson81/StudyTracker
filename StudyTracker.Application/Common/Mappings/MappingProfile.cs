using AutoMapper;
using StudyTracker.Application.DTOs;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mappningar Entity -> DTO. DTO:erna är records med positionella parametrar
        // vilket AutoMapper löser via ConstructUsingServiceLocator / ForCtorParam.
        CreateMap<Student, StudentDto>();
        CreateMap<StudySession, StudySessionDto>();
    }
}
