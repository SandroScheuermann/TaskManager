using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Domain.Entities.Shared;

namespace TaskManager.Domain.Entities
{
    public class Project : MongoEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        public required string? ProjectName { get; set; }
    }
}
