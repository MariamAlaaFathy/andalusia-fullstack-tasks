using Microsoft.AspNetCore.Mvc;
using TaskNine.Model;
using TaskNine.Services.Interfaces;

namespace TaskNine.Controllers
{
    /// <summary>
    /// Manages users that tasks can be assigned to.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private IUsersService _userService;

        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets a paginated, filterable list of users.
        /// </summary>
        /// <param name="paginationParams">Page, page size, and any available filters.</param>
        /// <response code="200">The paginated list of users.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<Users>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterParams paginationParams)
        {
            return Ok(await _userService.GetUsers(paginationParams));
        }

        /// <summary>
        /// Gets a single user by id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <response code="200">The requested user.</response>
        /// <response code="404">No user exists with the given id.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The new user's name.</param>
        /// <response code="201">The user was created.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody] Users user)
        {
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Replaces an existing user's details.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="user">The full replacement user's name.</param>
        /// <response code="200">The updated user.</response>
        /// <response code="404">No user exists with the given id.</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users user)
        {
            await _userService.UpdateUser(id, user);
            return Ok(user);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <response code="204">The user was deleted.</response>
        /// <response code="404">No user exists with the given id.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
