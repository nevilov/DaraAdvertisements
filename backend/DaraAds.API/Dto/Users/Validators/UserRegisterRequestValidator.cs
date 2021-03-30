using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class UserRegisterRequestValidator: AbstractValidator<UserRegisterRequest>
    {
        const int MAX_LENGTH= 100;
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Введите email")
                .MaximumLength(MAX_LENGTH).WithMessage("Email может содержать не более 100 символов");
            
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username не может быть пустым")
                .MaximumLength(MAX_LENGTH).WithMessage("Username может содержать не более 100 символов");
            
            RuleFor(x => x.Password)
                .Matches("[A-Z]").WithMessage("Пароль должен содержать заглавные буквы")
                .Matches("[a-z]").WithMessage("Пароль должен содержать буквы с нижним регистром")
                .Matches("[0-9]").WithMessage("Пароль должен содержать цифры")
                .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать особый символы");

            //RuleFor(x => x.Name)
            //    .NotEmpty().WithMessage("Имя не может быть пустым")
            //    .MaximumLength(MAX_LENGTH).WithMessage("Имя не может быть больше 100 символов");
            
            //RuleFor(x => x.LastName)
            //    .NotEmpty().WithMessage("Фамилия не может быть пустым")
            //    .MaximumLength(MAX_LENGTH).WithMessage("Фамилия не может быть больше 100 символов");

            //RuleFor(x => x.Phone)
            //    .NotEmpty().WithMessage("Номер телефона не может быть пустым")
            //    .MaximumLength(MAX_LENGTH).WithMessage("Номер телефона не может быть более 100 символов");
        }
    }
}