using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class UserRegisterRequestValidator: AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Введите email")
                .NotEmpty().WithMessage("Email не может быть пустым")
                .MaximumLength(100).WithMessage("Email может содержать не более 100 символов");
            
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username не может быть пустым")
                .MaximumLength(100).WithMessage("Username может содержать не более 100 символов");
            
            RuleFor(x => x.Password)
                .Matches("[A-Z]").WithMessage("Пароль должен содержать заглавные буквы")
                .Matches("[a-z]").WithMessage("Пароль должен содержать буквы с нижним регистром")
                .Matches("[0-9]").WithMessage("Пароль должен содержать цифры")
                .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать особый символы");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не может быть пустым")
                .MaximumLength(100).WithMessage("Имя не может быть больше 100 символов");
            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Фамилия не может быть пустым")
                .MaximumLength(100).WithMessage("Фамилия не может быть больше 100 символов");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Номер телефона не может быть пустым")
                .MaximumLength(100).WithMessage("Номер телефона не может быть более 100 символов");
        }
    }
}