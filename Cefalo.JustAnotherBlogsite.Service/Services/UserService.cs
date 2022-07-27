using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Repository.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<User?> GetUserByUserIdAsync(int userId)
        {
            return await _userRepository.GetUserByUserIdAsync(userId);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<User?> UpdateUserAsync(int userId, User updatedUser)
        {
            updatedUser.UpdatedAt = DateTime.UtcNow;
            return await _userRepository.UpdateUserAsync(userId, updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            User? user = await _userRepository.GetUserByUserIdAsync(userId);

            if(user == null)
            {
                return false;
            }

            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
