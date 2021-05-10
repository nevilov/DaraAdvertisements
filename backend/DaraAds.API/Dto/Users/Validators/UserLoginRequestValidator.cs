using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        const int MAX_LENGTH = 100;
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин не может быть пустым")
                .MaximumLength(MAX_LENGTH).WithMessage($"Логин может содержать не более {MAX_LENGTH} символов");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль не может быть пустым");
        }
    }
}