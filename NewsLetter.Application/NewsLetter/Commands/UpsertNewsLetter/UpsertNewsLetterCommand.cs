using MediatR;

namespace NewsLetter.Application.NewsLetter.Commands.UpsertNewsLetter
{
    public class UpsertNewsLetterCommand : IRequest<Core.Entities.NewsLetter>
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public string? HtmlBody { get; set; }
    }
}
