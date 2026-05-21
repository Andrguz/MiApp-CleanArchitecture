using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MiApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Registra de forma automática todos los Handlers de MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // Registra de forma automática todos los Validadores de FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}