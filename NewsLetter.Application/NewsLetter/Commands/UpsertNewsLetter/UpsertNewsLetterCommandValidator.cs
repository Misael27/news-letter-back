using FluentValidation;

namespace NewsLetter.Application.NewsLetter.Commands.UpsertNewsLetter
{
    public class UpsertNewsLetterCommandValidator : AbstractValidator<UpsertNewsLetterCommand>
    {
        public UpsertNewsLetterCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.HtmlBody).NotEmpty().NotNull();
        }
    }
}
