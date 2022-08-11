using AutoMapper;
using Cefalo.JustAnotherBlogsite.Database.Models;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.AutoMapperProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDetailsDto>()
                 .ForMember(
                dest => dest.AuthorUsername,
                opt => opt.MapFrom(src => src.Author.Username));

            CreateMap<BlogPostDto, Blog>()
                .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
