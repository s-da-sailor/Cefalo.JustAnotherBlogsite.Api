using Cefalo.JustAnotherBlogsite.Database.Context;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.JustAnotherBlogsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<string>> SignupAsync(User user)
        {
            string token = await _authService.SignupAsync(user);
            return Ok(token);
        }
    }
}
