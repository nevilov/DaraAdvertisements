using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class ChangeRoleRequestValidator: AbstractValidator<ChangeRoleRequest>
    {
        public ChangeRoleRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не может быть пустым")
                .MaximumLength(100).WithMessage("Email может содержать не более 100 символов");
                
            RuleFor(x => x.NewRole)
                .NotEmpty().WithMessage("Новая роль не может быть пустая")
                .MaximumLength(100).WithMessage("Новая роль не может содержать более 100 символов");
        }
    }
}