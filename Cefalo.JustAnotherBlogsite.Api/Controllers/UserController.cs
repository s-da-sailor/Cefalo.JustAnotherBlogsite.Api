using Cefalo.JustAnotherBlogsite.Database.Context;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cefalo.JustAnotherBlogsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDetailsDto>>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserAsync(int userId)
        {
            var user = await _userService.GetUserByUserIdAsync(userId);
            return Ok(user);
        }

        [HttpPut("{userId}"), Authorize]
        public async Task<ActionResult<List<UserDetailsDto>>> UpdateUserAsync(int userId, UserUpdateDto updatedUser)
        {
            var updatedUserDetails = await _userService.UpdateUserAsync(userId, updatedUser);

            return Ok(updatedUserDetails);
        }

        [HttpDelete("{userId}"), Authorize]
        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            var user = await _userService.DeleteUserAsync(userId);

            if (user == false)
            {
                return NotFound("Invalid Request.");
            }

            return NoContent();
        }
    }
}
