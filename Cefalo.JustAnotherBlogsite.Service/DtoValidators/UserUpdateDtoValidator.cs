using Cefalo.JustAnotherBlogsite.Service.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.DtoValidators
{
    public class UserUpdateDtoValidator : BaseDtoValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(b => b.FullName)
                .NotEmpty()
                .Length(1, 50)
                .Matches("^[a-zA-Z ]*$").WithMessage("'Full Name' must be consisted of letters and spaces only.");

            RuleFor(b => b.Email)
                .NotEmpty()
                .Length(1, 320)
                .EmailAddress();

            RuleFor(b => b.Password)
                .NotEmpty()
                .Length(8, 50);
        }
    }
}
