using AgroSolutions.History.Application.Interfaces;
using AgroSolutions.History.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgroSolutions.History.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISensorService, SensorService>();
        return services;
    }
}