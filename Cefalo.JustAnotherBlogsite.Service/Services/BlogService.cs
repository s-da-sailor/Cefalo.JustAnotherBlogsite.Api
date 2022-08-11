﻿using AutoMapper;
using Cefalo.JustAnotherBlogsite.Database.Models;
using Cefalo.JustAnotherBlogsite.Repository.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Cefalo.JustAnotherBlogsite.Service.DtoValidators;
using Cefalo.JustAnotherBlogsite.Service.Exceptions;
using Cefalo.JustAnotherBlogsite.Service.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BaseDtoValidator<BlogPostDto> _blogPostDtoValidator;
        private readonly BaseDtoValidator<BlogUpdateDto> _blogUpdateDtoValidator;

        public BlogService(IBlogRepository blogRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, 
            BaseDtoValidator<BlogPostDto> blogPostDtoValidator, BaseDtoValidator<BlogUpdateDto> blogUpdateDtoValidator)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _blogPostDtoValidator = blogPostDtoValidator;
            _blogUpdateDtoValidator = blogUpdateDtoValidator;
        }

        public async Task<BlogDetailsDto> PostBlogAsync(BlogPostDto newBlog)
        {
            _blogPostDtoValidator.ValidateDto(newBlog);

            Blog blog = _mapper.Map<Blog>(newBlog);

            if (_httpContextAccessor.HttpContext != null)
            {
                var currentUserIdString = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                blog.AuthorId = Int32.Parse(currentUserIdString);
            }
            else
            {
                throw new BadRequestException("Bad Request.");
            }

            Blog createdBlog = await _blogRepository.CreateBlogAsync(blog);
            var blogDetails = _mapper.Map<BlogDetailsDto>(createdBlog);

            return blogDetails;
        }

        public async Task<List<BlogDetailsDto>> GetBlogsAsync()
        {
            var blogs = await _blogRepository.GetBlogsAsync();

            var blogDetailsList = _mapper.Map<List<BlogDetailsDto>>(blogs);

            return blogDetailsList;
        }

        public async Task<BlogDetailsDto> GetBlogByBlogIdAsync(int blogId)
        {
            var blog = await _blogRepository.GetBlogByBlogIdAsync(blogId);

            if (blog == null)
            {
                throw new NotFoundException("Blog not found.");
            }

            var blogDetails = _mapper.Map<BlogDetailsDto>(blog);

            return blogDetails;
        }

        public async Task<BlogDetailsDto> UpdateBlogAsync(int blogId, BlogUpdateDto blogDetails)
        {
            _blogUpdateDtoValidator.ValidateDto(blogDetails);

            var blog = await _blogRepository.GetBlogByBlogIdAsync(blogId);

            if (blog == null)
            {
                throw new NotFoundException("Blog not found.");
            }

            if (_httpContextAccessor.HttpContext != null)
            {
                var currentUserIdString = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUserRole = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                
                //@TODO Change IsUserAuthorized for Blog (if there is an error)
                var tokenGenerationTimeString = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Expiration)?.Value;
                var tokenGenerationTime = Convert.ToDateTime(tokenGenerationTimeString);

                if (!AuthChecker.IsUserAuthorized(currentUserIdString, blog.Author.UserId.ToString(), currentUserRole))
                {
                    throw new ForbiddenException("You are not authorized.");
                }

                if (AuthChecker.IsTokenExpired(tokenGenerationTime, blog.Author.PasswordChangedAt))
                {
                    throw new UnauthorizedException("Token is expired. Log in again.");
                }
            }
            else
            {
                throw new BadRequestException("Bad Request.");
            }

            blog.Title = blogDetails.Title;
            blog.Description = blogDetails.Description;
            blog.UpdatedAt = DateTime.UtcNow;

            var updatedBlog = await _blogRepository.UpdateBlogAsync(blogId, blog);

            var updatedBlogDetails = _mapper.Map<BlogDetailsDto>(updatedBlog);

            return updatedBlogDetails;
        }

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            var blog = await _blogRepository.GetBlogByBlogIdAsync(blogId);

            if (blog == null)
            {
                throw new NotFoundException("Blog not found.");
            }

            if (_httpContextAccessor.HttpContext != null)
            {
                var currentUserIdString = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUserRole = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

                var tokenGenerationTime = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Expiration)?.Value;
                var passwordChangedAt = Convert.ToDateTime(tokenGenerationTime);

                if (!AuthChecker.IsUserAuthorized(currentUserIdString, blog.Author.UserId.ToString(), currentUserRole))
                {
                    throw new ForbiddenException("You are not authorized.");
                }

                if (AuthChecker.IsTokenExpired(passwordChangedAt, blog.Author.PasswordChangedAt))
                {
                    throw new UnauthorizedException("Token is expired. Log in again.");
                }
            }
            else
            {
                throw new BadRequestException("Bad Request.");
            }

            return await _blogRepository.DeleteBlogAsync(blog);
        }
    }
}