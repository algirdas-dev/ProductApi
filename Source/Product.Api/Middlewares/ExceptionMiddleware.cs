using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _loggerFactory;
    public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var logger = _loggerFactory.CreateLogger("ExceptionMiddleware");
            logger.LogError($"Something went wrong: {ex}", "");
            HandleExceptionAsync(httpContext, ex);
        }
    }
    private void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
}