using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Application.Common.Constants;

namespace StudyTracker.Infrastructure.Identity;

// Skapar rollerna Admin/User samt en default Admin-användare vid uppstart.
// Admin-användarens lösenord läses från konfiguration (Seed:AdminPassword).
// Om rollerna eller användaren redan finns hoppar den över steget.
public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var configuration = services.GetRequiredService<IConfiguration>();

        foreach (var role in new[] { Roles.Admin, Roles.User })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminUserName = configuration["Seed:AdminUserName"] ?? "admin";
        var adminEmail = configuration["Seed:AdminEmail"] ?? "admin@studytracker.local";
        var adminPassword = configuration["Seed:AdminPassword"] ?? "Admin123!";

        var admin = await userManager.FindByNameAsync(adminUserName);
        if (admin is null)
        {
            admin = new ApplicationUser { UserName = adminUserName, Email = adminEmail };
            var createResult = await userManager.CreateAsync(admin, adminPassword);
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Roles.Admin);
            }
        }
    }
}
