using FluentValidation;

namespace ProjectX.Dto
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }

    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName).MaximumLength(80).MinimumLength(4).NotEmpty();
            RuleFor(x => x.UserName).MaximumLength(80).MinimumLength(4).NotEmpty();
            RuleFor(X => X.Email).MinimumLength(7).MaximumLength(256).NotEmpty();
            RuleFor(x => x.Password).MaximumLength(256).MinimumLength(8).NotEmpty();
            RuleFor(x => x.CheckPassword).Equal(x => x.Password).WithMessage("Password do not match!");

            //RuleFor(x => x).Custom((x, context) =>
            //{
            //    if(x.Password != x.CheckPassword)
            //    {
            //        context.AddFailure("CheckPassword", "Password doesn't match!");
            //    }
            //});

        }
    }
}
