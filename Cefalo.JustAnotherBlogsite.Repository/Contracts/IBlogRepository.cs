﻿using Cefalo.JustAnotherBlogsite.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Repository.Contracts
{
    public interface IBlogRepository
    {
        public Task<Blog?> CreateBlogAsync(Blog blog);
        public Task<List<Blog>> GetBlogsAsync();
        public Task<Blog?> GetBlogByBlogIdAsync(int blogId);
        public Task<Blog?> UpdateBlogAsync(int blogId, Blog updatedBlog);
        public Task<bool> DeleteBlogAsync(Blog blog);
    }
}
