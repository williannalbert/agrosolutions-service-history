using AgroSolutions.History.API.Configuration;
using AgroSolutions.History.API.Middlewares;
using AgroSolutions.History.Application.Extensions;  
using AgroSolutions.History.Infrastructure.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();
