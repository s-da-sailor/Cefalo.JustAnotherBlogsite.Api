using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Repository.Contracts
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<List<User>> GetUsersAsync(int pageNumber, int pageSize);
        public Task<int> GetUserCountAsync();
        public Task<User?> GetUserByUserIdAsync(int userId);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<User?> UpdateUserAsync(int userId, User updatedUser);
        public Task<bool> DeleteUserAsync(User user);
        public Task<List<User>> SearchUserAsync(int pageNumber, int pageSize, string searchParam);
        public Task<int> GetSearchUserCountAsync(string searchParam);
        public Task<List<Blog>> GetUserSpecificBlogsAsync(int pageNumber, int pageSize, int userId);
        public Task<int> GetUserSpecificBlogCountAsync(int userId);
    }
}
