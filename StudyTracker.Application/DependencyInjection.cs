using Microsoft.Extensions.DependencyInjection;

namespace StudyTracker.Application;

public static class DependencyInjection
{
    // Registrerar MediatR och scannar denna assembly efter handlers.
    // Anropas från Program.cs i API-lagret.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}
