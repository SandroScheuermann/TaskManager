using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Domain.Entities.Shared;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities.Assignments
{
    public class Assignment : MongoEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public required string? ProjectId { get; set; }

        public required string? Title { get; set; }

        public required string? Description { get; set; }

        public List<Comment>? Comments { get; set; } = [];

        public required DateTime? ExpirationDate { get; set; }

        public required AssignmentStatusEnum? Status { get; set; }

        public required AssignmentPriorityEnum? Priority { get; set; }
    }
}
