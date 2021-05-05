using FluentValidation;

namespace DaraAds.API.Dto.Users.Validators
{
    public class ChangeRoleRequestValidator: AbstractValidator<ChangeRoleRequest>
    {

        public ChangeRoleRequestValidator()
        {           
            RuleFor(x => x.NewRole)
                .NotEmpty().WithMessage("Новая роль не может быть пустая")
                .MaximumLength(100).WithMessage("Новая роль не может содержать более 100 символов");
        }
    }
}