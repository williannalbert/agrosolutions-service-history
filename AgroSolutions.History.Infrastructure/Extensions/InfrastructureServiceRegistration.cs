using AgroSolutions.History.Domain.Interfaces;
using AgroSolutions.History.Infrastructure.Configuration;
using AgroSolutions.History.Infrastructure.Persistence.Context;
using AgroSolutions.History.Infrastructure.Persistence.Mappings;
using AgroSolutions.History.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroSolutions.History.Infrastructure.Extensions;
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<MongoDbContext>();
        services.AddScoped<ISensorRepository, SensorRepository>();

        MongoClassMap.RegisterClassMaps();

        return services;
    }
}