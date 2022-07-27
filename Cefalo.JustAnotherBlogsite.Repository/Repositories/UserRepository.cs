using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Database.Context;
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

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
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

            if (user == null)
            {
                return user;
            }

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.UpdatedAt = updatedUser.UpdatedAt;
            user.Password = updatedUser.Password;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
