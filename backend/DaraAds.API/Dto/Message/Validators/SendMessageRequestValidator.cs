using FluentValidation;
namespace DaraAds.API.Dto.Message.Validators
{
    public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
    {
        public SendMessageRequestValidator()
        {
            RuleFor(a => a.Text)
                .NotEmpty().WithMessage("Сообщение не может быть пустым")
                .MaximumLength(250).WithMessage("Сообщение не может превышать 250 символов");

            RuleFor(a => a.ChatId).NotEmpty().WithMessage("ChatId не может быть пустым");
            RuleFor(a => a.RecipientId).NotEmpty().WithMessage("RecipientId не может быть пустым");
        }
    }
}
