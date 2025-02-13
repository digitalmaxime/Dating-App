using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;

namespace API.Dtos;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Password).MinimumLength(8);
        // RuleFor(x => x.Username).Must(name => Regex.IsMatch(name, "\w+$")).WithMessage("usernmae should match 'asdf'");
    }
}
