using Microsoft.EntityFrameworkCore;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Database;

namespace StudyTracker.Infrastructure.Repositories.StudySessionRepository;

public class StudySessionRepo : IStudySessionRepository
{
    private readonly AppDbContext _context;

    public StudySessionRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudySession>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.StudySessions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<StudySession?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.StudySessions
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<StudySession> AddAsync(StudySession session, CancellationToken cancellationToken)
    {
        _context.StudySessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);
        return session;
    }

    public async Task UpdateAsync(StudySession session, CancellationToken cancellationToken)
    {
        _context.StudySessions.Update(session);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var session = await _context.StudySessions.FindAsync(new object[] { id }, cancellationToken);
        if (session is null) return;

        _context.StudySessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
