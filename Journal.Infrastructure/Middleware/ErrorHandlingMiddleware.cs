using log4net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Journal.Infrastructure.Middleware;

public class ErrorHandlingMiddleware:IMiddleware
{
    private readonly ILog _log;

    public ErrorHandlingMiddleware(ILog log)
    {
        _log = log;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (FluentValidation.ValidationException exception)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            _log.Error(exception);
            var validationErrors = exception.Errors.Select(error => new
            {
                Field = error.PropertyName,
                Error = error.ErrorMessage
            });

            var jsonResponse = JsonConvert.SerializeObject(new { Errors = validationErrors });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}