using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NewsLetter.Core.Entities;
using System.Xml.Linq;

namespace NewsLetter.Infrastructure
{
    public class DataContext
    {
        public IMongoDatabase Database { get; }

        public DataContext(IOptions<NewsLetterDatabaseSettings> newsLetterDatabaseSettings)
        {
            var mongoClient = new MongoClient(
           newsLetterDatabaseSettings.Value.ConnectionString);

            Database = mongoClient.GetDatabase(
                newsLetterDatabaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(ReadPreference? readPreference = null) where TEntity : class
        {
            return Database
              .WithReadPreference(readPreference ?? ReadPreference.Primary)
              .GetCollection<TEntity>(GetCollectionName<TEntity>());
        }

        public static string? GetCollectionName<TEntity>() where TEntity : class
        {
            return (typeof(TEntity).GetCustomAttributes(typeof(MongoCollectionAttribute), true).FirstOrDefault() as MongoCollectionAttribute)?.CollectionName;
        }

    }
}
