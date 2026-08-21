using Microsoft.AspNetCore.Mvc;
using TaskEight.Exceptions;

namespace TaskFour.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            catch (InvalidIdException ex)
            {
                _logger.LogError(ex, "InvalidIdException occurred.");
                await WriteProblemDetails(context, 400, "Invalid task ID.", ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "NotFoundException occurred.");
                await WriteProblemDetails(context, 404, "Task not found.", ex.Message);
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex, "ConflictException occurred.");
                await WriteProblemDetails(context, 409, "Task already exists.", ex.Message);
            }
            catch (DueDateInPastException ex)
            {
                _logger.LogError(ex, "DueDateInPastException occurred.");
                await WriteProblemDetails(context, 422, "Invalid due date.", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await WriteProblemDetails(context, 500, "An unexpected error occurred.", "Please contact support.");
            }
        }

        public async Task WriteProblemDetails(HttpContext context, int status, string title, string message)
        {
            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";
            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = message
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
