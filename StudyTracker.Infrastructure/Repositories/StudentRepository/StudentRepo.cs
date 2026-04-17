using Microsoft.EntityFrameworkCore;
using StudyTracker.Application.Interfaces;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Database;

namespace StudyTracker.Infrastructure.Repositories.StudentRepository;

public class StudentRepo : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken)
    {
        // Include Sessions så relationen syns i API-svaret.
        return await _context.Students
            .Include(s => s.Sessions)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Students
            .Include(s => s.Sessions)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Student> AddAsync(Student student, CancellationToken cancellationToken)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);
        return student;
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var student = await _context.Students.FindAsync(new object[] { id }, cancellationToken);
        if (student is null) return;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
