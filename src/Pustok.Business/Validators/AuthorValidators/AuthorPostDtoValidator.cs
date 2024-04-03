using FluentValidation;
using Pustok.Business.DTOs.AuthorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Validators.AuthorValidators
{
    public class AuthorPostDtoValidator : AbstractValidator<AuthorPostDto>
    {
        public AuthorPostDtoValidator()
        {
            RuleFor(a=>a.Fullname)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(250);
        }
    }
}
