using Cefalo.JustAnotherBlogsite.Database.Context;
using Cefalo.JustAnotherBlogsite.Database.Models;
using Cefalo.JustAnotherBlogsite.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Repository.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;

        public BlogRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Blog?> CreateBlogAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return await _context.Blogs.Include(u => u.Author).FirstOrDefaultAsync(u => u.BlogId == blog.BlogId);
        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await _context.Blogs.Include(u => u.Author).ToListAsync();
        }

        public async Task<Blog?> GetBlogByBlogIdAsync(int blogId)
        {
            return await _context.Blogs.Include(u => u.Author).FirstOrDefaultAsync(u => u.BlogId == blogId);
        }

        public async Task<Blog?> UpdateBlogAsync(int blogId, Blog updatedBlog)
        {
            var blog = await _context.Blogs.Include(u => u.Author).FirstOrDefaultAsync(u => u.BlogId == blogId);

            blog.Title = updatedBlog.Title;
            blog.Description = updatedBlog.Description;
            blog.UpdatedAt = updatedBlog.UpdatedAt;

            await _context.SaveChangesAsync();

            return blog;
        }

        public async Task<bool> DeleteBlogAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
