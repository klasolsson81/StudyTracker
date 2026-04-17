namespace StudyTracker.Application.Common.Constants;

// Konstanter för att undvika magic strings i [Authorize(Roles = "...")] och seeding.
public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
}
