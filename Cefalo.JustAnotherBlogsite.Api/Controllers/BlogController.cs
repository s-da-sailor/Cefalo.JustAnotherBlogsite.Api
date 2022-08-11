using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.JustAnotherBlogsite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<BlogDetailsDto>> PostBlogAsync(BlogPostDto request)
        {
            var blog = await _blogService.PostBlogAsync(request);
            return Ok(blog);
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogDetailsDto>>> GetBlogsAsync()
        {
            var blogs = await _blogService.GetBlogsAsync();
            return Ok(blogs);
        }

        [HttpGet("{blogId}")]
        public async Task<ActionResult<BlogDetailsDto>> GetBlogAsync(int blogId)
        {
            var blog = await _blogService.GetBlogByBlogIdAsync(blogId);
            return Ok(blog);
        }

        [HttpPut("{blogId}"), Authorize]
        public async Task<ActionResult<List<BlogDetailsDto>>> UpdateBlogAsync(int blogId, BlogUpdateDto updatedBlog)
        {
            var updatedBlogDetails = await _blogService.UpdateBlogAsync(blogId, updatedBlog);

            return Ok(updatedBlogDetails);
        }

        [HttpDelete("{blogId}"), Authorize]
        public async Task<ActionResult> DeleteBlogAsync(int blogId)
        {
            var blog = await _blogService.DeleteBlogAsync(blogId);

            if (blog == false)
            {
                return NotFound("Invalid Request.");
            }

            return NoContent();
        }
    }
}