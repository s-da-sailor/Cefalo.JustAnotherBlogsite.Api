using Cefalo.JustAnotherBlogsite.Service.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.DtoValidators
{
    public class BaseDtoValidator<T> : AbstractValidator<T>
    {
        public void ValidateDto(T dto)
        {
            var ValidationResult = this.Validate(dto);

            if(!ValidationResult.IsValid)
            {
                string ErrorMessage = "";

                foreach(var error in ValidationResult.Errors)
                {
                    ErrorMessage += error.ToString();
                }

                throw new BadRequestException(ErrorMessage);
            }
        }
    }
}
