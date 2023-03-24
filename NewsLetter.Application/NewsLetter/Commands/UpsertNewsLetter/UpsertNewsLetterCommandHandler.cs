using MediatR;
using NewsLetter.Core;
using NewsLetter.Application.ValidationHandle.Exceptions;
using NewsLetter.Core.IRepositories;
using System.Threading.Tasks;

namespace NewsLetter.Application.NewsLetter.Commands.UpsertNewsLetter
{
    public class CreateOrUpdateNewsLetterommandHandler : IRequestHandler<UpsertNewsLetterCommand, Core.Entities.NewsLetter>
    {

        private readonly INewsLetterRepository _newsLetterRepository;

        public CreateOrUpdateNewsLetterommandHandler(INewsLetterRepository newsLetterRepository)
        {
            _newsLetterRepository = newsLetterRepository;
        }

        async Task<Core.Entities.NewsLetter> IRequestHandler<UpsertNewsLetterCommand, Core.Entities.NewsLetter>.Handle(UpsertNewsLetterCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null) //Create mode
            {
                return await CreateNewsLetter(request);
            }
            else 
            {
                return await UpdateNewsLetter(request);
            }
        }

        public async Task<Core.Entities.NewsLetter> CreateNewsLetter(UpsertNewsLetterCommand request)
        {
            var newsLetter = new Core.Entities.NewsLetter
            {
                Title = request.Title,
                HtmlBody = request.HtmlBody,
                CreatedAt = DateTime.UtcNow
            };
            await _newsLetterRepository.CreateAsync(newsLetter);
            return newsLetter;
        }

        public async Task<Core.Entities.NewsLetter> UpdateNewsLetter(UpsertNewsLetterCommand request)
        {
            var newsLetter = await _newsLetterRepository.GetAsync(request.Id);
            if (newsLetter == null)
            {
                throw new NotFoundException("NewsLetter", request.Id.ToString());
            }
            newsLetter.Title = request.Title;
            newsLetter.HtmlBody = request.HtmlBody;
            await _newsLetterRepository.UpdateAsync(request.Id, newsLetter);
            return newsLetter;
        }

    }
}
