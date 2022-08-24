﻿using AutoMapper;
using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Repository.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Cefalo.JustAnotherBlogsite.Service.DtoValidators;
using Cefalo.JustAnotherBlogsite.Service.Exceptions;
using Cefalo.JustAnotherBlogsite.Service.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Services
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BaseDtoValidator<UserUpdateDto> _userUpdateDtoValidator;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, BaseDtoValidator<UserUpdateDto> userUpdateDtoValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userUpdateDtoValidator = userUpdateDtoValidator;
        }
        public async Task<List<UserDetailsDto>> GetUsersAsync(int pageNumber, int pageSize)
        {
            var users =  await _userRepository.GetUsersAsync(pageNumber, pageSize);

            var userDetailsList = _mapper.Map<List<UserDetailsDto>>(users);

            return userDetailsList;
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _userRepository.GetUserCountAsync();
        }

        public async Task<UserDetailsDto> GetUserByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByUserIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var userDetails = _mapper.Map<UserDetailsDto>(user);

            return userDetails;
        }

        public async Task<UserDetailsDto> GetUserByUsernameAsync(string username)
        {
            var user =  await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var userDetails = _mapper.Map<UserDetailsDto>(user);

            return userDetails;
        }

        public async Task<UserDetailsDto> UpdateUserAsync(int userId, UserUpdateDto userDetails)
        {
            _userUpdateDtoValidator.ValidateDto(userDetails);

            var user = await _userRepository.GetUserByUserIdAsync(userId);

            if(user == null)
            {
                throw new NotFoundException("User not found.");
            }

            if (_httpContextAccessor.HttpContext != null)
            {
                var currentUserIdString = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUserRole = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

                var tokenGenerationTimeString = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Expiration)?.Value;
                var tokenGenerationTime = Convert.ToDateTime(tokenGenerationTimeString);

                if (!AuthChecker.IsUserAuthorized(currentUserIdString, userId.ToString(), currentUserRole))
                {
                    throw new ForbiddenException("You are not authorized.");
                }

                if (AuthChecker.IsTokenExpired(tokenGenerationTime, user.PasswordChangedAt))
                {
                    throw new UnauthorizedException("Token is expired. Log in again.");
                }
            }
            else
            {
                throw new BadRequestException("Bad Request.");
            }

            user.FullName = userDetails.FullName;
            user.Email = userDetails.Email;
            user.UpdatedAt = DateTime.UtcNow;

            if (userDetails.Password != null)
            {
                PasswordHash.CreatePasswordHash(userDetails.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.PasswordChangedAt = DateTime.UtcNow;
            }

            var updatedUser =  await _userRepository.UpdateUserAsync(userId, user);

            var updatedUserDetails = _mapper.Map<UserDetailsDto>(updatedUser);

            return updatedUserDetails;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByUserIdAsync(userId);

            if(user == null)
            {
                throw new NotFoundException("User not found.");
            }

            if(_httpContextAccessor.HttpContext != null)
            {
                var currentUserIdString = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUserRole = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

                var tokenGenerationTime = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Expiration)?.Value;
                var passwordChangedAt = Convert.ToDateTime(tokenGenerationTime);

                if (!AuthChecker.IsUserAuthorized(currentUserIdString, userId.ToString(), currentUserRole))
                {
                    throw new ForbiddenException("You are not authorized.");
                }

                if (AuthChecker.IsTokenExpired(passwordChangedAt, user.PasswordChangedAt))
                {
                    throw new UnauthorizedException("Token is expired. Log in again.");
                }
            } else
            {
                throw new BadRequestException("Bad Request.");
            }

            return await _userRepository.DeleteUserAsync(user);
        }

        public async Task<List<UserDetailsDto>> SearchUserAsync(int pageNumber, int pageSize, string searchParam)
        {
            var users = await _userRepository.SearchUserAsync(pageNumber, pageSize, searchParam);

            var userDetailsList = _mapper.Map<List<UserDetailsDto>>(users);

            return userDetailsList;
        }

        public async Task<int> GetSearchUserCountAsync(string searchParam)
        {
            return await _userRepository.GetSearchUserCountAsync(searchParam);
        }

        public async Task<List<BlogDetailsDto>> GetUserSpecificBlogsAsync(int pageNumber, int pageSize, int userId)
        {
            var blogs = await _userRepository.GetUserSpecificBlogsAsync(pageNumber, pageSize, userId);

            var blogDetailsList = _mapper.Map<List<BlogDetailsDto>>(blogs);

            return blogDetailsList;
        }

        public async Task<int> GetUserSpecificBlogCountAsync(int userId)
        {
            return await _userRepository.GetUserSpecificBlogCountAsync(userId);
        }
    }
}
