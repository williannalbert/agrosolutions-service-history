using AgroSolutions.History.API.Middlewares;
using AgroSolutions.History.Application.Extensions;  
using AgroSolutions.History.Infrastructure.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() 
               .AllowAnyMethod()  
               .AllowAnyHeader(); 
    });
});

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Host.UseSerilog((context, configuration) =>
{
    var elasticUri = context.Configuration["ElasticConfiguration:Uri"] ?? "http://localhost:9200";

    configuration
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console() 
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
        {
            IndexFormat = $"agro-history-logs-{DateTime.UtcNow:yyyy.MM.dd}",
            AutoRegisterTemplate = true, 
            NumberOfShards = 2,
            NumberOfReplicas = 1
        })
        .ReadFrom.Configuration(context.Configuration); 
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
