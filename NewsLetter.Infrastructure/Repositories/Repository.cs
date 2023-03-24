using System.Linq.Expressions;
using NewsLetter.Core.IRepositories;
using MongoDB.Driver;
using NewsLetter.Core.Entities;
using System.Collections.Generic;

namespace NewsLetter.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IMongoEntity
    {

        protected readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAsync() =>
        await _context.GetCollection<TEntity>().Find(_ => true).ToListAsync();

        public async Task<TEntity?> GetAsync(string id) =>
            await _context.GetCollection<TEntity>().Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TEntity newTEntity) =>
            await _context.GetCollection<TEntity>().InsertOneAsync(newTEntity);

        public async Task UpdateAsync(string id, TEntity updatedTEntity) =>
            await _context.GetCollection<TEntity>().ReplaceOneAsync(x => x.Id == id, updatedTEntity);

        public async Task RemoveAsync(string id) =>
            await _context.GetCollection<TEntity>().DeleteOneAsync(x => x.Id == id);

    }
}
