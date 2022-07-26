﻿using Cefalo.JustAnotherBlogsite.Api;
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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetUsersAsync(int pageNumber, int pageSize)
        {
            return await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<User?> GetUserByUserIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> UpdateUserAsync(int userId, User updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.UpdatedAt = updatedUser.UpdatedAt;
            user.PasswordHash = updatedUser.PasswordHash;
            user.PasswordSalt = updatedUser.PasswordSalt;
            user.PasswordChangedAt = updatedUser.PasswordChangedAt;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> SearchUserAsync(int pageNumber, int pageSize, string searchParam)
        {
            return await _context.Users
                .Where(user => !string.IsNullOrEmpty(user.Username) && user.Username.Contains(searchParam))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetSearchUserCountAsync(string searchParam)
        {
            return await _context.Users
                .Where(user => !string.IsNullOrEmpty(user.Username) && user.Username.Contains(searchParam))
                .CountAsync();
        }

        public async Task<List<Blog>> GetUserSpecificBlogsAsync(int pageNumber, int pageSize, int userId)
        {
            return await _context.Blogs
                .Where(u => u.AuthorId == userId)
                .Include(u => u.Author)
                .OrderByDescending(u => u.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetUserSpecificBlogCountAsync(int userId)
        {
            return await _context.Blogs
                .Where(u => u.AuthorId == userId)
                .CountAsync();
        }
    }
}
