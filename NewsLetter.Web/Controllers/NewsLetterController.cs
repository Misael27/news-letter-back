using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.NewsLetter.Commands.DeleteNewsLetter;
using NewsLetter.Application.NewsLetter.Commands.UpsertNewsLetter;
using NewsLetter.Application.NewsLetter.Queries.GetNewsLetter;

namespace NewsLetter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsLetterController : ControllerBase
    {
        private IMediator _mediator;
        public NewsLetterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<Core.Entities.NewsLetter> GetById(string id)
        {
            return await _mediator.Send(new GetNewsLetterQuery(id));
        }

        [HttpPost("upsert")]
        public async Task<Core.Entities.NewsLetter> AddOrUpdateNewsLetter(UpsertNewsLetterCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<Unit> DeleteById(string id)
        {
            return await _mediator.Send(new DeleteNewsLetterCommand(id));
        }

    }
}
