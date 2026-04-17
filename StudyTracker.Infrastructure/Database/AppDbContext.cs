using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Identity;

namespace StudyTracker.Infrastructure.Database;

// Ärver IdentityDbContext för att få tabellerna AspNetUsers, AspNetRoles, AspNetUserRoles osv.
// ApplicationUser är vår egen subklass av IdentityUser.
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudySession> StudySessions => Set<StudySession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Student-tabellen: krav på namn/email/klass, email är unik.
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Email).IsRequired().HasMaxLength(200);
            entity.Property(s => s.Class).IsRequired().HasMaxLength(50);
            entity.HasIndex(s => s.Email).IsUnique();
        });

        // StudySession: FK till Student, motivation 1-10, notes valfria.
        // Cascade delete — raderas student så raderas dess sessions.
        modelBuilder.Entity<StudySession>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Subject).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Notes).HasMaxLength(1000);

            entity.HasOne(s => s.Student)
                  .WithMany(student => student.Sessions)
                  .HasForeignKey(s => s.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
