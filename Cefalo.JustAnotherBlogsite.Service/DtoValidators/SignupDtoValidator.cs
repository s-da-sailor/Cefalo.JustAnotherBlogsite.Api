using Cefalo.JustAnotherBlogsite.Service.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.DtoValidators
{
    public class SignupDtoValidator : BaseDtoValidator<SignupDto>
    {
         public SignupDtoValidator()
        {
            RuleFor(b => b.Username)
                .NotEmpty()
                .Length(1, 30)
                .Matches("^[a-z0-9]+$").WithMessage("'Username' must be consisted of small letters and numbers without space.");

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

            RuleFor(b => b.Role)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(2);
        }
    }
}
