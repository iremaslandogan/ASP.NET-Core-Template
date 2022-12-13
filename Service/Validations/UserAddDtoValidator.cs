using Core.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        public UserAddDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("BADREQUEST").NotEmpty().WithMessage("BADREQUEST");
            RuleFor(x => x.Lastname).NotNull().WithMessage("BADREQUEST").NotEmpty().WithMessage("BADREQUEST");
            RuleFor(x => x.Phone).NotNull().WithMessage("BADREQUEST").NotEmpty().WithMessage("BADREQUEST");
            //RuleFor(x => x.)
        }
    }
}
