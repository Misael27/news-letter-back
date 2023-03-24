using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using NewsLetter.Core.Entities;

namespace NewsLetter.Core.Entities
{
    [MongoCollectionAttribute("NewsLetter")]
    public class NewsLetter : IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? HtmlBody { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
