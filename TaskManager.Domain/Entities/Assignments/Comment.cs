using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TaskManager.Domain.Entities.Assignments
{
    public class Comment
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string? Id { get; set; }

        public required string? Content { get; set; }  
    }
}
