using Cefalo.JustAnotherBlogsite.Database.Context;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cefalo.JustAnotherBlogsite.Api.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserAsync(int userId)
        {
            var user = await _userService.GetUserByUserIdAsync(userId);

            if(user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<List<User>>> UpdateUserAsync(int userId, User updatedUser)
        {
            var user = await _userService.GetUserByUserIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _userService.UpdateUserAsync(userId, updatedUser);

            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            var user = await _userService.DeleteUserAsync(userId);

            if (user == false)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }
    }*/
}
