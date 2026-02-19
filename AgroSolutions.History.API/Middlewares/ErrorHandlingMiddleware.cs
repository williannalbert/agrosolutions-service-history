using AgroSolutions.History.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace AgroSolutions.History.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex); 
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse();

        switch (exception)
        {
            case DomainException domainEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = domainEx.Message;
                response.ErrorType = "DomainValidation";
                _logger.LogWarning("Violação de regra de domínio: {Message}", domainEx.Message);
                break;
            case ArgumentException argEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = argEx.Message;
                response.ErrorType = "InvalidArguments";
                _logger.LogWarning("Argumento inválido: {Message}", argEx.Message);
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "Ocorreu um erro interno no servidor.";
                response.ErrorType = "InternalServerError";
                _logger.LogError(exception, "Erro não tratado na aplicação.");
                break;
        }

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonResult = JsonSerializer.Serialize(response, jsonOptions);

        return context.Response.WriteAsync(jsonResult);
    }
}
public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public string ErrorType { get; set; } = string.Empty;
}