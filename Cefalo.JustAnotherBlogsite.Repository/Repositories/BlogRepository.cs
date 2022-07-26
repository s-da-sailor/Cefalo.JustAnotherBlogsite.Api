﻿using Cefalo.JustAnotherBlogsite.Database.Context;
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

            var createdBlog = await _context.Blogs.Include(u => u.Author).FirstOrDefaultAsync(u => u.BlogId == blog.BlogId);
            return createdBlog;
        }

        public async Task<List<Blog>> GetBlogsAsync(int pageNumber, int pageSize)
        {
            return await _context.Blogs
                .Include(u => u.Author)
                .OrderByDescending(u => u.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetBlogCountAsync()
        {
            return await _context.Blogs.CountAsync();
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
