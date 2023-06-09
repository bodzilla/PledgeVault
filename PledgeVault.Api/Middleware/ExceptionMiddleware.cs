using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PledgeVault.Core.Exceptions;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        if (exception is PledgeVaultException)
        {
            context.Response.StatusCode = exception switch
            {
                InvalidRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => context.Response.StatusCode
            };
        }
        else context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = exception.Message }));
    }
}
