using MediatR;

namespace NewsLetter.Application.NewsLetter.Commands.DeleteNewsLetter
{
    public class DeleteNewsLetterCommand : IRequest<Unit>
    {
        public string Id { get; set; }

        public DeleteNewsLetterCommand(string id)
        {
            Id = id;
        }

    }
}
