using FluentValidation;
using SamuraiLegend.Application.DTOs.Authentication;

namespace SamuraiLegend.Application.Validations.AuthenticationValidation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(user => user.Email).EmailAddress();

            RuleFor(user => user.Password).Password();
        }
    }
}
