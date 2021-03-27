using FluentValidation;

namespace DaraAds.API.Dto.Advertisement.Validators
{
    public class AdvertisementUpdateRequestValidator: AbstractValidator<AdvertisementUpdateRequest>
    {
        public AdvertisementUpdateRequestValidator()
        {
            RuleFor(a => a.Title)
                .MaximumLength(100).WithMessage("Заголовок не может быть более 100 символов")
                .NotEmpty().WithMessage("Заголовок не может быть пустым");

            RuleFor(a => a.Description)
                .MaximumLength(10000).WithMessage("Описание не может привышать 10 000 символов")
                .NotEmpty().WithMessage("Описание не может быть пустым");

            RuleFor(a => a.Price)
                .NotEmpty().WithMessage("Цена не может быть пустой");

            RuleFor(a => a.CategoryId)
                .NotEmpty();
        }
    }
}