using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Application.Interfaces;
using StudyTracker.Infrastructure.Database;
using StudyTracker.Infrastructure.Repositories.StudentRepository;
using StudyTracker.Infrastructure.Repositories.StudySessionRepository;

namespace StudyTracker.Infrastructure;

public static class DependencyInjection
{
    // Registrerar DbContext + repositories. Anropas från Program.cs i API-lagret
    // så att API inte behöver känna till EF Core-detaljer.
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IStudentRepository, StudentRepo>();
        services.AddScoped<IStudySessionRepository, StudySessionRepo>();

        return services;
    }
}
