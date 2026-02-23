using AgroSolutions.History.API.Middlewares;
using AgroSolutions.History.Application.Extensions;
using AgroSolutions.History.Infrastructure.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace AgroSolutions.History.API.Configuration;

public static class ApiConfiguration
{
    public static void AddApiConfiguration(this WebApplicationBuilder builder)
    {
        ConfigureSerilog(builder);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", p =>
            {
                p.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();
            });
        });

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddAuthenticationConfiguration(builder.Configuration);
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }

    private static void ConfigureSerilog(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            var elasticUri = context.Configuration["ElasticConfiguration:Uri"] ?? "http://localhost:9200";

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    IndexFormat = $"agro-history-logs-{DateTime.UtcNow:yyyy-MM-dd}",
                    AutoRegisterTemplate = true,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                })
                .ReadFrom.Configuration(context.Configuration);
        });
    }
}
