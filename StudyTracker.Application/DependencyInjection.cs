using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Application.Common.Behaviors;

namespace StudyTracker.Application;

public static class DependencyInjection
{
    // Registrerar MediatR, AutoMapper, FluentValidation och ValidationBehavior.
    // ValidationBehavior registreras FÖRE andra behaviors så att ogiltiga requests
    // stoppas innan andra pipeline-steg (logging, caching, etc.) körs.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
