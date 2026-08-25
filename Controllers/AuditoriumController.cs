using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingProject.Controllers
{
    /// <summary>
    /// Manages cinema auditoriums and exposes the showtimes scheduled within them.
    /// </summary>
    [ApiController]
    [Route("api/auditoriums")]
    [Produces("application/json")]
    public class AuditoriumsController : ControllerBase
    {
        private readonly IAuditoriumService _auditoriumService;
        private readonly IShowTimeService _showTimeService;

        public AuditoriumsController(IAuditoriumService auditoriumService, IShowTimeService showTimeService)
        {
            _auditoriumService = auditoriumService;
            _showTimeService = showTimeService;
        }

        /// <summary>
        /// Gets a paginated list of auditoriums.
        /// </summary>
        /// <param name="paginationParams">Page and page size (pageSize capped at 100).</param>
        /// <response code="200">The paginated list of auditoriums.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AuditoriumDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AuditoriumDTO>>> GetAuditoriums([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _auditoriumService.GetAuditoriums(paginationParams));
        }

        /// <summary>
        /// Gets a single auditorium by id.
        /// </summary>
        /// <param name="id">The auditorium id.</param>
        /// <response code="200">The requested auditorium.</response>
        /// <response code="404">No auditorium exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AuditoriumDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuditoriumDTO>> GetAuditoriumById(int id)
        {
            return Ok(await _auditoriumService.GetAuditoriumById(id));
        }

        /// <summary>
        /// Creates a new auditorium.
        /// </summary>
        /// <param name="auditorium">The room number, capacity, and availability of the new auditorium.</param>
        /// <response code="201">The auditorium was created. The response includes a Location header pointing to it.</response>
        /// <response code="400">The request body failed validation.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AuditoriumDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuditoriumDTO>> CreateAuditorium([FromBody] CreateAuditoriumRequest auditorium)
        {
            var createdAuditorium = await _auditoriumService.CreateAuditorium(auditorium);
            return CreatedAtAction(nameof(GetAuditoriumById), new { id = createdAuditorium.Id }, createdAuditorium);
        }

        /// <summary>
        /// Replaces an existing auditorium's details.
        /// </summary>
        /// <param name="id">The auditorium id.</param>
        /// <param name="auditorium">The full replacement room number, capacity, and availability.</param>
        /// <response code="200">The updated auditorium.</response>
        /// <response code="400">The request body failed validation.</response>
        /// <response code="404">No auditorium exists with the given id.</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(AuditoriumDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuditoriumDTO>> UpdateAuditorium(int id, [FromBody] UpdateAuditoriumRequest auditorium)
        {
            return Ok(await _auditoriumService.UpdateAuditorium(id, auditorium));
        }

        /// <summary>
        /// Deletes an auditorium.
        /// </summary>
        /// <param name="id">The auditorium id.</param>
        /// <response code="204">The auditorium was deleted.</response>
        /// <response code="404">No auditorium exists with the given id.</response>
        /// <response code="409">The auditorium has scheduled showtimes and cannot be deleted.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteAuditorium(int id)
        {
            await _auditoriumService.DeleteAuditorium(id);
            return NoContent();
        }

        /// <summary>
        /// Gets the showtimes scheduled in a specific auditorium, optionally filtered to a single date.
        /// </summary>
        /// <param name="id">The auditorium id.</param>
        /// <param name="date">Optional date filter - only showtimes on this date are returned.</param>
        /// <response code="200">The list of matching showtimes.</response>
        /// <response code="404">No auditorium exists with the given id.</response>
        [HttpGet("{id}/showtimes")]
        [ProducesResponseType(typeof(List<ShowTimeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ShowTimeDTO>>> GetShowTimesForAuditorium(int id, [FromQuery] DateTime? date)
        {
            return Ok(await _showTimeService.GetShowTimesByAuditorium(id, date));
        }
    }
}
