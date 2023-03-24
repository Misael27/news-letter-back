using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NewsLetter.Core.Entities
{
    public interface IMongoEntity
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
