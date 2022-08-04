using AutoMapper;
using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailsDto>();

            CreateMap<SignupDto, User>()
                .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                dest => dest.PasswordChangedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}

