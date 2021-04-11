using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Токен не может быть пустым");

            RuleFor(x => x.NewPassword)
            .Matches("[A-Z]").WithMessage("Пароль должен содержать заглавные буквы")
            .Matches("[a-z]").WithMessage("Пароль должен содержать буквы с нижним регистром")
            .Matches("[0-9]").WithMessage("Пароль должен содержать цифры")
            .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать особый символы (!, @, #, $, %...)")
            .Equal(x => x.RepeatedPassword).WithMessage("Пароли не совпадают");
        }
    }
}
