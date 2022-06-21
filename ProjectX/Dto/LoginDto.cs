using FluentValidation;

namespace ProjectX.Dto
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(80).MinimumLength(4).NotEmpty();
            RuleFor(x => x.Password).MaximumLength(256).MinimumLength(8).NotEmpty();
        }
    }
}
