using MediatR;
using NewsLetter.Application.ValidationHandle.Exceptions;
using NewsLetter.Core.IRepositories;

namespace NewsLetter.Application.NewsLetter.Commands.DeleteNewsLetter
{
    public class DeleteNewsLetterCommandHandler : IRequestHandler<DeleteNewsLetterCommand, Unit>
    {

        private readonly INewsLetterRepository _newsLetterRepository;

        public DeleteNewsLetterCommandHandler(INewsLetterRepository newsLetterRepository)
        {
            _newsLetterRepository = newsLetterRepository;
        }

        public async Task<Unit> Handle(DeleteNewsLetterCommand request, CancellationToken cancellationToken)
        {
            var newsLetter = await _newsLetterRepository.GetAsync(request.Id);
            if (newsLetter == null)
            {
                throw new NotFoundException("NewsLetter", request.Id.ToString());
            }
            await _newsLetterRepository.RemoveAsync(request.Id);
            return Unit.Value;
        }
    }
}
