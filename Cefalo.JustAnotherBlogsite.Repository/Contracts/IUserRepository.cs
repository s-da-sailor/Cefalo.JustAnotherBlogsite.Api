using Cefalo.JustAnotherBlogsite.Api;
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
        public Task<User?> GetUserByUserIdAsync(int userId);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<User?> UpdateUserAsync(int userId, User updatedUser);
        public Task<bool> DeleteUserAsync(User user);
    }
}
