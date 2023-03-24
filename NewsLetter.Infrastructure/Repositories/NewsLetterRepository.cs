using MongoDB.Driver;
using NewsLetter.Core.IRepositories;

namespace NewsLetter.Infrastructure.Repositories
{
    public class NewsLetterRepository : Repository<Core.Entities.NewsLetter>, INewsLetterRepository
    {
        public NewsLetterRepository(DataContext context) : base(context)
        {
        }

        public DataContext? DataContext
        {
            get { return _context; }
        }
    }
}
