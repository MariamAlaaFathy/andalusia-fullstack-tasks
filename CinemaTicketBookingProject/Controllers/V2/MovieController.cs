using Asp.Versioning;
using CinemaTicketBookingProject.DTOs;
using CinemaTicketBookingProject.Model;
using CinemaTicketBookingProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingProject.Controllers.V2
{
    /// <summary>
    /// Version 2 (current) - detailed movie representation: id, name, genre, releaseDate, availableInCinema.
    /// Full CRUD.
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/movies")]
    [Produces("application/json")]
    public class MoviesV2Controller : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesV2Controller(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Gets a paginated, filterable, sortable list of movies (detailed shape).
        /// </summary>
        /// <param name="filterParams">Search, genre filter, sortBy (name/releaseDate), order (asc/desc), page, pageSize.</param>
        /// <response code="200">The paginated list of movies.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<MovieDTOV2>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<MovieDTOV2>>> GetMovies([FromQuery] MovieFilterParams filterParams)
        {
            return Ok(await _movieService.GetMoviesV2(filterParams));
        }

        /// <summary>
        /// Gets a single movie by id (detailed shape).
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <response code="200">The requested movie.</response>
        /// <response code="404">No movie exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(MovieDTOV2), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDTOV2>> GetMovieById(int id)
        {
            return Ok(await _movieService.GetMovieByIdV2(id));
        }

        /// <summary>
        /// Gets a single movie by exact name (detailed shape).
        /// </summary>
        /// <param name="name">The movie's exact name.</param>
        /// <response code="200">The requested movie.</response>
        /// <response code="404">No movie exists with the given name.</response>
        [HttpGet]
        [Route("name={name}")]
        [ProducesResponseType(typeof(MovieDTOV2), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDTOV2>> GetMovieByName(string name)
        {
            return Ok(await _movieService.GetMovieByNameV2(name));
        }

        /// <summary>
        /// Creates a new movie. Duplicate titles are rejected.
        /// </summary>
        /// <param name="movie">The name, genre, release date, and cinema availability.</param>
        /// <response code="201">The movie was created. The response includes a Location header pointing to it.</response>
        /// <response code="400">The request body failed validation.</response>
        /// <response code="409">A movie with the same name already exists.</response>
        [HttpPost]
        [ProducesResponseType(typeof(MovieDTOV2), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<MovieDTOV2>> CreateMovie([FromBody] CreateMovieRequest movie)
        {
            var createdMovie = await _movieService.CreateMovie(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id}, createdMovie);
        }

        /// <summary>
        /// Replaces an existing movie's details.
        /// </summary>
        /// <param name="id">The movie id.</param>
        /// <param name="movie">The full replacement name, genre, release date, and cinema availability.</param>
        /// <response code="200">The updated movie.</response>
        /// <response code="400">The request body failed validation.</response>
        /// <response code="404">No movie exists with the given id.</response>
        /// <response code="409">Another movie already has the given name.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MovieDTOV2), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<MovieDTOV2>> UpdateMovie(int id, [FromBody] UpdateMovieRequest movie)
        {
            return Ok(await _movieService.UpdateMovie(id, movie));
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
