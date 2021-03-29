using FluentValidation;

namespace DaraAds.API.Dto.Advertisement.Validators
{
    public class AdvertisementUpdateRequestValidator: AbstractValidator<AdvertisementUpdateRequest>
    {
        const int MAX_LENGTH_TITLE = 100;
        const int MAX_LENGTH_DESCRIPTION = 1000;

        public AdvertisementUpdateRequestValidator()
        {
            RuleFor(a => a.Title)
                .MaximumLength(MAX_LENGTH_TITLE).WithMessage("Заголовок не может быть более 100 символов")
                .NotEmpty().WithMessage("Заголовок не может быть пустым");

            RuleFor(a => a.Description)
                .MaximumLength(MAX_LENGTH_DESCRIPTION).WithMessage("Описание не может привышать 10 000 символов")
                .NotEmpty().WithMessage("Описание не может быть пустым");

            RuleFor(a => a.Price)
                .NotEmpty().WithMessage("Цена не может быть пустой");

            RuleFor(a => a.CategoryId)
                .NotEmpty();
        }
    }
}