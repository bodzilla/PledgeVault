using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PledgeVault.Core.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PledgeVault.Api.Middleware;

internal sealed class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        object response;

        if (exception is PledgeVaultException)
        {
            switch (exception)
            {
                case InvalidRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return context.Response.StartAsync();
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return context.Response.StartAsync();
            }

            response = new { message = exception.Message };
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            response = new { message = exception.Message, innerException = exception.InnerException?.Message };
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
