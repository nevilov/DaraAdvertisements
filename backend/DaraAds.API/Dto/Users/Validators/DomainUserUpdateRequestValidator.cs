using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class DomainUserUpdateRequestValidator: AbstractValidator<DomainUserUpdateRequest>
    {
        public DomainUserUpdateRequestValidator()
        {
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