using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Application.Interfaces;
using StudyTracker.Infrastructure.Database;
using StudyTracker.Infrastructure.Identity;
using StudyTracker.Infrastructure.Repositories.StudentRepository;
using StudyTracker.Infrastructure.Repositories.StudySessionRepository;
using StudyTracker.Infrastructure.Services;

namespace StudyTracker.Infrastructure;

public static class DependencyInjection
{
    // Registrerar DbContext, Identity (ApplicationUser + IdentityRole), repositories
    // samt AuthService. Anropas från Program.cs i API-lagret så att API inte behöver
    // känna till EF Core- eller Identity-detaljer.
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' saknas i konfigurationen.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IStudentRepository, StudentRepo>();
        services.AddScoped<IStudySessionRepository, StudySessionRepo>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
