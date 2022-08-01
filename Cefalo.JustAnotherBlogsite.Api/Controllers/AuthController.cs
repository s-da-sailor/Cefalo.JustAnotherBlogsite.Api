using Cefalo.JustAnotherBlogsite.Database.Context;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.JustAnotherBlogsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<string>> SignupAsync(SignupDto request)
        {
            string token = await _authService.SignupAsync(request);
            return Ok(token);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginDto request)
        {
            string token = await _authService.LoginAsync(request);
            return Ok(token);
        }

        [HttpGet, Authorize(Roles = "2")]
        [Route("test")]
        public async Task<ActionResult<string>> TestRoute()
        {
            return Ok("Yo this is a test");
        }
    }
}
