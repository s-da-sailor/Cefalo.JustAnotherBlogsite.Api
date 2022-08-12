using Cefalo.JustAnotherBlogsite.Api.Filters;
using Cefalo.JustAnotherBlogsite.Api.Wrappers;
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
        public async Task<ActionResult<Response<BlogDetailsDto>>> PostBlogAsync(BlogPostDto request)
        {
            var blog = await _blogService.PostBlogAsync(request);
            var response = new Response<BlogDetailsDto>(blog);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<BlogDetailsDto>>>> GetBlogsAsync([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var blogs = await _blogService.GetBlogsAsync(validFilter.PageNumber, validFilter.PageSize);
            var pagedResponse = new PagedResponse<List<BlogDetailsDto>>(blogs, validFilter.PageNumber, validFilter.PageSize);
            pagedResponse.TotalRecords = blogs.Count;
            return Ok(pagedResponse);
        }

        [HttpGet("{blogId}")]
        public async Task<ActionResult<Response<BlogDetailsDto>>> GetBlogAsync(int blogId)
        {
            var blog = await _blogService.GetBlogByBlogIdAsync(blogId);
            var response = new Response<BlogDetailsDto>(blog);
            return Ok(response);
        }

        [HttpPut("{blogId}"), Authorize]
        public async Task<ActionResult<Response<BlogDetailsDto>>> UpdateBlogAsync(int blogId, BlogUpdateDto updatedBlog)
        {
            var updatedBlogDetails = await _blogService.UpdateBlogAsync(blogId, updatedBlog);
            var response = new Response<BlogDetailsDto>(updatedBlogDetails);

            return Ok(response);
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