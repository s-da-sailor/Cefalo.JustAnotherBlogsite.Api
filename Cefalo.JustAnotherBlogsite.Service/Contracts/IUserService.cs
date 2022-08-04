﻿using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Contracts
{
    public interface IUserService
    {
        public Task<List<UserDetailsDto>> GetUsersAsync();
        public Task<UserDetailsDto> GetUserByUserIdAsync(int userId);
        public Task<UserDetailsDto> GetUserByUsernameAsync(string username);
        public Task<UserDetailsDto> UpdateUserAsync(int userId, UserUpdateDto updatedUser);
        public Task<bool> DeleteUserAsync(int userId);
    }
}
