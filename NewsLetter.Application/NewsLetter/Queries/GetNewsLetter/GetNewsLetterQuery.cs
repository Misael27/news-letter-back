using MediatR;

namespace NewsLetter.Application.NewsLetter.Queries.GetNewsLetter
{
    public class GetNewsLetterQuery : IRequest<Core.Entities.NewsLetter>
    {
        public string Id;
        public GetNewsLetterQuery(string id) 
        {
            Id = id;
        }
    }

}
