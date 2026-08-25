using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingProject.Controllers
{
    /// <summary>
    /// Manages movie showtimes and their bookings.
    /// </summary>
    [ApiController]
    [Route("api/showtimes")]
    [Produces("application/json")]
    public class ShowTimesController : ControllerBase
    {
        private readonly IShowTimeService _showTimeService;
        private readonly IBookingService _bookingService;

        public ShowTimesController(IShowTimeService showTimeService, IBookingService bookingService)
        {
            _showTimeService = showTimeService;
            _bookingService = bookingService;
        }

        /// <summary>
        /// Gets a paginated list of showtimes.
        /// </summary>
        /// <param name="paginationParams">Page and page size (pageSize capped at 100).</param>
        /// <response code="200">The paginated list of showtimes.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ShowTimeDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<ShowTimeDTO>>> GetShowTimes([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _showTimeService.GetShowTimes(paginationParams));
        }

        /// <summary>
        /// Gets a single showtime by id.
        /// </summary>
        /// <param name="id">The showtime id.</param>
        /// <response code="200">The requested showtime.</response>
        /// <response code="404">No showtime exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ShowTimeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowTimeDTO>> GetShowTimeById(int id)
        {
            return Ok(await _showTimeService.GetShowTimeById(id));
        }

        /// <summary>
        /// Gets showtimes for a specific auditorium, optionally filtered to a single date.
        /// </summary>
        /// <param name="auditoriumId">The auditorium id.</param>
        /// <param name="date">Optional date filter - only showtimes on this date are returned.</param>
        /// <response code="200">The list of matching showtimes.</response>
        /// <response code="404">No auditorium exists with the given id.</response>
        [HttpGet]
        [Route("auditorium={auditoriumId}")]
        [ProducesResponseType(typeof(List<ShowTimeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ShowTimeDTO>>> GetShowTimesByAuditorium(int auditoriumId, [FromQuery] DateTime? date)
        {
            return Ok(await _showTimeService.GetShowTimesByAuditorium(auditoriumId, date));
        }

        /// <summary>
        /// Schedules a new showtime for a movie in an auditorium.
        /// </summary>
        /// <param name="showTime">The movie, auditorium, and date/time to schedule.</param>
        /// <response code="201">The showtime was created. The response includes a Location header pointing to its bookings endpoint.</response>
        /// <response code="400">The request body failed validation, or the movie/auditorium is not currently available for scheduling.</response>
        /// <response code="404">No movie or auditorium exists with the given id.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ShowTimeDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowTimeDTO>> CreateShowTime([FromBody] CreateShowTimeRequest showTime)
        {
            var createdShowTime = await _showTimeService.CreateShowTime(showTime);
            return CreatedAtAction(nameof(GetBookingsForShowTime), new { id = createdShowTime.Id }, createdShowTime);
        }

        /// <summary>
        /// Replaces an existing showtime's details.
        /// </summary>
        /// <param name="id">The showtime id.</param>
        /// <param name="showTime">The full replacement movie, auditorium, and date/time.</param>
        /// <response code="200">The updated showtime.</response>
        /// <response code="400">The request body failed validation.</response>
        /// <response code="404">No showtime, movie, or auditorium exists with the given id.</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ShowTimeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowTimeDTO>> UpdateShowTime(int id, [FromBody] UpdateShowTimeRequest showTime)
        {
            return Ok(await _showTimeService.UpdateShowTime(id, showTime));
        }

        /// <summary>
        /// Deletes a showtime.
        /// </summary>
        /// <param name="id">The showtime id.</param>
        /// <response code="204">The showtime was deleted.</response>
        /// <response code="404">No showtime exists with the given id.</response>
        /// <response code="409">The showtime has active bookings and cannot be deleted.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteShowTime(int id)
        {
            await _showTimeService.DeleteShowTime(id);
            return NoContent();
        }

        /// <summary>
        /// Gets booking details, including customer info, for a specific showtime.
        /// </summary>
        /// <param name="id">The showtime id.</param>
        /// <response code="200">The list of bookings for the showtime.</response>
        /// <response code="404">No showtime exists with the given id.</response>
        [HttpGet]
        [Route("{id}/bookings")]
        [ProducesResponseType(typeof(List<BookingDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BookingDTO>>> GetBookingsForShowTime(int id)
        {
            return Ok(await _bookingService.GetBookingsByShowTime(id));
        }
    }
}
