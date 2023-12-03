using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Domain.Entities.Shared;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Assignment : MongoEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public required string ProjectId { get; set; }

        public required string? Title { get; set; }

        public required string? Description { get; set; }

        public IEnumerable<string>? Comments { get; set; }

        public required DateTime? ExpirationDate { get; set; }

        public required AssignmentStatusEnum? Status { get; set; }

        public required AssignmentPriorityEnum? Priority { get; set; }

        public override string ToString()
        {
            return $"Id : {Id}, Título : {Title}, Status: {Status}";
        }
    }
}
