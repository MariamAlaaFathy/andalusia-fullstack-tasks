using FullStackSession6.Model;
using Microsoft.AspNetCore.Mvc;
using TaskEight.Model;
using TaskEight.Services.Interfaces;

namespace TaskEight.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private IUsersService _userService;

        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterParams paginationParams)
        {
            return Ok(await _userService.GetUsers(paginationParams));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Users user)
        {
            await _userService.CreateUser(user);
            return Created($"api/users/{user.Id}", user);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users user)
        {
            await _userService.UpdateUser(id, user);
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
