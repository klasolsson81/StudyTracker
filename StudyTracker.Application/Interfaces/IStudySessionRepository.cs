using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Interfaces;

public interface IStudySessionRepository
{
    Task<IEnumerable<StudySession>> GetAllAsync(CancellationToken cancellationToken);
    Task<StudySession?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<StudySession> AddAsync(StudySession session, CancellationToken cancellationToken);
    Task UpdateAsync(StudySession session, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
