using CinemaTicketBookingProject.Exceptions;
using Microsoft.AspNetCore.Mvc;

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
            catch (MovieNotFoundException ex)
            {
                _logger.LogError(ex, "MovieNotFoundExcption occurred.");
                await WriteProblemDetails(context, 404, "Movie not found.", ex.Message);
            }
            catch (BookingNotFoundException ex)
            {
                _logger.LogError(ex, "BookingNotFoundException occurred.");
                await WriteProblemDetails(context, 404, "Booking not found.", ex.Message);
            }
            catch (MovieAlreadyExistsException ex)
            {
                _logger.LogError(ex, "MovieAlreadyExistsException occurred.");
                await WriteProblemDetails(context, 409, "Movie already exists.", ex.Message);
            }
            catch (ShowTimeNotFoundException ex)
            {
                _logger.LogError(ex, "ShowTimeNotFoundException occurred.");
                await WriteProblemDetails(context, 404, "ShowTime not found.", ex.Message);
            }
            catch (CustomerNotFoundException ex)
            {
                _logger.LogError(ex, "CustomerNotFoundException occurred.");
                await WriteProblemDetails(context, 404, "Customer not found.", ex.Message);
            }
            catch (InvalidBookingException ex)
            {
                _logger.LogError(ex, "InvalidBookingException occurred.");
                await WriteProblemDetails(context, 400, "Customer not found.", ex.Message);
            }
            catch (AuditoriumNotFoundException ex)
            {
                _logger.LogError(ex, "AuditoriumNotFoundException occurred.");
                await WriteProblemDetails(context, 404, "Auditorium not found.", ex.Message);
            }
            catch (CustomerAlreadyExistsException ex)
            {
                _logger.LogError(ex, "CustomerAlreadyExistsException occurred.");
                await WriteProblemDetails(context, 409, "Customer already exists.", ex.Message);
            }
            catch (EntityShouldNotBeDeletedException ex)
            {
                _logger.LogError(ex, "EntityShouldNotBeDeletedException occured");
                await WriteProblemDetails(context, 409, "Cannot be deleted.", ex.Message);
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
