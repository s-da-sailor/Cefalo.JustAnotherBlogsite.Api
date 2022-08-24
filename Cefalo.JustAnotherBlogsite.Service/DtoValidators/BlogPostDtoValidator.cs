using Cefalo.JustAnotherBlogsite.Service.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.DtoValidators
{
    public class BlogPostDtoValidator : BaseDtoValidator<BlogPostDto>
    {
        public BlogPostDtoValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(b => b.Description)
                .NotEmpty()
                .Length(1, 100000);
        }
    }
}
