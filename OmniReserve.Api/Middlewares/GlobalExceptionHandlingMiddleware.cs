using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OmniReserve.Application.Common.Exceptions;
using OmniReserve.Domain.Exceptions; 

namespace OmniReserve.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió una excepción no manejada.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            var validationProblem = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Error de Validación",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Detail = "Se enviaron datos inválidos."
            };
            
            validationProblem.Extensions.Add("errors", validationException.Errors);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(validationProblem));
            return;
        }

        else if (exception is DomainException domainException)
        {
            var domainProblem = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Error de Dominio",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Detail = domainException.Message // Aquí se expone el mensaje "privado" diseñado en la entidad
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(domainProblem));
            return;
        }

        var genericProblem = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Error Interno del Servidor",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Detail = "Ha ocurrido un error inesperado al procesar la solicitud."
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        await context.Response.WriteAsync(JsonSerializer.Serialize(genericProblem));
    }
}