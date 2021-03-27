using System.Data;
using FluentValidation;

namespace DaraAds.API.Dto.Abuse.Validators
{
    public class CreateAbuseBindingValidator : AbstractValidator<CreateAbuseBinding>
    {
        public CreateAbuseBindingValidator()
        {
            RuleFor(x => x.AdvId)
                .NotEmpty().WithMessage("Id объявления не может быть пустым");

            RuleFor(x => x.AbuseText)
                .NotEmpty().WithMessage("Текст жалобы не может быть пустым")
                .MaximumLength(1000).WithMessage("Текст жалобы не может привышать 1000 символов");
        }
    }
}