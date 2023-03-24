
using NewsLetter.Core.Entities;

namespace NewsLetter.Core.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class, IMongoEntity
    {
        Task<List<TEntity>> GetAsync();

        Task<TEntity?> GetAsync(string id);

        Task CreateAsync(TEntity newTEntity);

        Task UpdateAsync(string id, TEntity updatedTEntity);

        Task RemoveAsync(string id);

    }
}
