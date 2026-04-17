using Microsoft.Extensions.DependencyInjection;

namespace StudyTracker.Application;

public static class DependencyInjection
{
    // Registrerar MediatR, AutoMapper och scannar denna assembly efter
    // handlers och mapping-profiler. Anropas från Program.cs i API-lagret.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));

        return services;
    }
}
