using Application.Exceptions.Abstractions;
using Application.Exceptions.CustomExceptions;
using System.Net;

namespace LibraryWebApp.Middleware
{
    public class GlobalExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionsHandlerMiddleware> _logger;

        public GlobalExceptionsHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionsHandlerMiddleware> logger)
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
            catch (AlreadyExistsException ex)
            {
                _logger.LogError(ex.Message);

                await HandleError(ex, context, HttpStatusCode.Conflict);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                await HandleError(ex, context, HttpStatusCode.NotFound);
            }
            catch (RemovalFailureException ex)
            {
                _logger.LogError(ex.Message);

                await HandleError(ex, context, HttpStatusCode.InternalServerError);
            }
            catch (CreationFailureException ex)
            {
                _logger.LogError(ex.Message);

                await HandleError(ex, context, HttpStatusCode.InternalServerError);
            }
            catch (IncorrectDataException ex)
            {
                _logger.LogError(ex.Message);

                await HandleError(ex, context, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                await HandleError(ex, context, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleError(Exception ex, HttpContext context, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
            }.ToString());
        }
    }
}
