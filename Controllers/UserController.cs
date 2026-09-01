using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskTen.DTOs;
using TaskTen.Enums;
using TaskTen.Model;
using TaskTen.Services.Interfaces;

namespace TaskTen.Controllers
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
        /// <response code="401">You are not authorized to do this action.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<Users>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<UserDTO>>> GetUsers([FromQuery] UserFilterParams paginationParams)
        {
            string _currentUserRole = User.FindFirst(ClaimTypes.Role)!.Value;
            if (_currentUserRole != "Admin")
            {
                return Unauthorized(new { title = "You are not authorized to do this action." });
            }
            return Ok(await _userService.GetUsers(paginationParams));
        }

        /// <summary>
        /// Gets a single user by id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <response code="200">The requested user.</response>
        /// <response code="401">You are not authorized to do this action.</response>
        /// <response code="404">No user exists with the given id.</response>
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, out var _currentUserId);
            string _currentUserRole = User.FindFirst(ClaimTypes.Role)!.Value;
            if (_currentUserId != id && _currentUserRole != "Admin")
            {
                return Unauthorized(new { message = "You are not authorized to do this action." });
            }
            return Ok(await _userService.GetUserById(id));
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The new user's name, email, password, and role.</param>
        /// <response code="200">The user was registered.</response>
        /// <response code="409">A user with the same email already exists.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDTO user)
        {
            await _userService.RegisterUser(user);
            return Ok(new { message = "Registration successful." });
        }

        /// <summary>
        /// Logs in a new user.
        /// </summary>
        /// <param name="user">The new user's name, email, password, and role.</param>
        /// <response code="200">The user was logged in.</response>
        /// <response code="401">Invalid Credentials</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequestDTO user)
        {
            string token = await _userService.LoginUser(user);

            if (token == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            return Ok(new { message = "Login successful.", token });
        }

        /// <summary>
        /// Replaces an existing user's details.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="user">The full replacement user's name, email, password, and role.</param>
        /// <response code="200">The updated user.</response>
        /// <response code="401">You are not authorized to do this action.</response>
        /// <response code="404">No user exists with the given id.</response>
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, [FromBody] UpdateUserRequestDTO user)
        {
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, out var _currentUserId);
            string _currentUserRole = User.FindFirst(ClaimTypes.Role)!.Value;
            if (_currentUserId != id && _currentUserRole != "Admin")
            {
                return Unauthorized(new { message = "You are not authorized to do this action." });
            }
            if (_currentUserRole != "Admin")
            {
                user.Role = Role.User.ToString();
            }
            await _userService.UpdateUser(id, user);
            return Ok(user);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <response code="204">The user was deleted.</response>
        /// <response code="401">You are not authorized to do this action.</response>
        /// <response code="404">No user exists with the given id.</response>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, out var _currentUserId);
            string _currentUserRole = User.FindFirst(ClaimTypes.Role)!.Value;
            if (_currentUserId != id && _currentUserRole != "Admin")
            {
                return Unauthorized(new { message = "You are not authorized to do this action." });
            }
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
