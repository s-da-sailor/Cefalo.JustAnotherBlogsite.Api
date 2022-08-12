using Cefalo.JustAnotherBlogsite.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Contracts
{
    public interface IBlogService
    {
        public Task<BlogDetailsDto> PostBlogAsync(BlogPostDto newBlog);
        public Task<List<BlogDetailsDto>> GetBlogsAsync(int pageNumber, int pageSize);
        public Task<BlogDetailsDto> GetBlogByBlogIdAsync(int blogId);
        public Task<BlogDetailsDto> UpdateBlogAsync(int blogId, BlogUpdateDto updatedBlog);
        public Task<bool> DeleteBlogAsync(int blogId);
    }
}
