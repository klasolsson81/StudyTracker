using MediatR;

namespace StudyTracker.Application.StudySessionCRUD.Commands.DeleteStudySession;

public record DeleteStudySessionCommand(int Id) : IRequest<bool>;
