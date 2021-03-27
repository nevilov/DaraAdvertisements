using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин не может быть пустым")
                .MaximumLength(100).WithMessage("Логин может содержать не более 100 символов");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль не может быть пустым");
        }
    }
}