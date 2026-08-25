using Asp.Versioning;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingProject.Controllers.V1
{
    /// <summary>
    /// Version 1 (deprecated) - compact movie representation: id, name, availableInCinema.
    /// Read/delete only; use v2 for create/update.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/movies")]
    [Produces("application/json")]
    public class MoviesV1Controller : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesV1Controller(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Gets a paginated, filterable, sortable list of movies (compact shape).
        /// </summary>
        /// <param name="paginationParams">Search, genre filter, sortBy (name/releaseDate), order (asc/desc), page, pageSize.</param>
        /// <response code="200">The paginated list of movies.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<MovieDTOV1>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<MovieDTOV1>>> GetMovies([FromQuery] MovieFilterParams paginationParams)
        {
            return Ok(await _movieService.GetMoviesV1(paginationParams));
        }

        /// <summary>
        /// Gets a single movie by id (compact shape).
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <response code="200">The requested movie.</response>
        /// <response code="404">No movie exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(MovieDTOV1), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDTOV1>> GetMovieById(int id)
        {
            return Ok(await _movieService.GetMovieByIdV1(id));
        }

        /// <summary>
        /// Gets a single movie by exact name (compact shape).
        /// </summary>
        /// <param name="name">The movie's exact name.</param>
        /// <response code="200">The requested movie.</response>
        /// <response code="404">No movie exists with the given name.</response>
        [HttpGet]
        [Route("name={name}")]
        [ProducesResponseType(typeof(MovieDTOV1), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDTOV1>> GetMovieByName(string name)
        {
            return Ok(await _movieService.GetMovieByNameV1(name));
        }

        /// <summary>
        /// Deletes a movie.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <response code="204">The movie was deleted.</response>
        /// <response code="404">No movie exists with the given id.</response>
        /// <response code="409">The movie has scheduled showtimes and cannot be deleted.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _movieService.DeleteMovie(id);
            return NoContent();
        }
    }
}
