using Domain.Exceptions.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Domain.Exceptions
{
    public class GlobalExceptionsHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionsHandler> _logger;

        public GlobalExceptionsHandler(RequestDelegate next, ILogger<GlobalExceptionsHandler> logger)
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
                _logger.LogError(ex.Message);

                await HandleError(ex, context);
            }
        }

        private async Task HandleError(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
            }.ToString());
        }
    }
}
