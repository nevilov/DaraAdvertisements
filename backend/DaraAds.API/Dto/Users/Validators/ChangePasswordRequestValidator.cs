using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.NewPassword)
                .Matches("[A-Z]").WithMessage("Пароль должен содержать заглавные буквы")
                .Matches("[a-z]").WithMessage("Пароль должен содержать буквы с нижним регистром")
                .Matches("[0-9]").WithMessage("Пароль должен содержать цифры")
                .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать особый символы")
                .Equal(x => x.RepeatedNewPassword).WithMessage("Пароли не совпадают");

            RuleFor(x => x.OldPassword)
                .NotEmpty();
        }
    }
}