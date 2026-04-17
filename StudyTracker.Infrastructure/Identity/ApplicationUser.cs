using Microsoft.AspNetCore.Identity;

namespace StudyTracker.Infrastructure.Identity;

// Ärver IdentityUser för att få hantering av lösenord, claims, roles, tokens etc.
// Placeras i Infrastructure-lagret eftersom ASP.NET Identity är en infrastrukturdetalj
// och Domain ska inte bero på Microsoft.AspNetCore.Identity.
public class ApplicationUser : IdentityUser
{
}
