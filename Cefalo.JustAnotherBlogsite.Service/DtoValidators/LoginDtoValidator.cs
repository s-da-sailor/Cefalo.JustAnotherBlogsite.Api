using Cefalo.JustAnotherBlogsite.Service.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.DtoValidators
{
    public class LoginDtoValidator : BaseDtoValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(b => b.Username)
                .NotEmpty()
                .Length(1, 30)
                .Matches("^[a-z0-9]+$").WithMessage("'Username' must be consisted of small letters and numbers without space.");

            RuleFor(b => b.Password)
                .NotEmpty()
                .Length(8, 50);
        }
    }
}
