using MediatR;
using NewsLetter.Application.NewsLetter.Queries.GetNewsLetter;
using NewsLetter.Application.ValidationHandle.Exceptions;
using NewsLetter.Core;
using NewsLetter.Core.IRepositories;

namespace NewsLetter.Application.Tasks.Queries.GetAllTasks
{
    public class GetNewsLetterQueryHandler : IRequestHandler<GetNewsLetterQuery, Core.Entities.NewsLetter>
    {
        private readonly INewsLetterRepository _newsLetterRepository;

        public GetNewsLetterQueryHandler(INewsLetterRepository newsLetterRepository)
        {
            _newsLetterRepository = newsLetterRepository;
        }

        public async Task<Core.Entities.NewsLetter> Handle(GetNewsLetterQuery request, CancellationToken cancellationToken)
        {
            var newsLetter = await _newsLetterRepository.GetAsync(request.Id);
            if (newsLetter == null)
            {
                throw new NotFoundException("NewsLetter", request.Id.ToString());
            }
            return newsLetter;
        }
    }
}
