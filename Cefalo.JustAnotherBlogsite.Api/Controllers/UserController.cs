using Cefalo.JustAnotherBlogsite.Api.Filters;
using Cefalo.JustAnotherBlogsite.Api.Wrappers;
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
        public async Task<ActionResult<PagedResponse<List<UserDetailsDto>>>> GetUsersAsync([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var users = await _userService.GetUsersAsync(validFilter.PageNumber, validFilter.PageSize);
            var pagedResponse = new PagedResponse<List<UserDetailsDto>>(users, validFilter.PageNumber, validFilter.PageSize);
            pagedResponse.TotalRecords = users.Count;
            return Ok(pagedResponse);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Response<UserDetailsDto>>> GetUserAsync(int userId)
        {
            var user = await _userService.GetUserByUserIdAsync(userId);
            var response = new Response<UserDetailsDto>(user);
            return Ok(response);
        }

        [HttpPut("{userId}"), Authorize]
        public async Task<ActionResult<Response<UserDetailsDto>>> UpdateUserAsync(int userId, UserUpdateDto updatedUser)
        {
            var updatedUserDetails = await _userService.UpdateUserAsync(userId, updatedUser);
            var response = new Response<UserDetailsDto>(updatedUserDetails);
            return Ok(response);
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
