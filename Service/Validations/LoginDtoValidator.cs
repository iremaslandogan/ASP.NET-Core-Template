using Core.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Phone).NotNull().WithMessage("BADREQUEST").NotEmpty().WithMessage("BADREQUEST");
            RuleFor(x => x.Password).NotNull().WithMessage("BADREQUEST").NotEmpty().WithMessage("BADREQUEST");
            //RuleFor(x => x.)
        }
    }
}
