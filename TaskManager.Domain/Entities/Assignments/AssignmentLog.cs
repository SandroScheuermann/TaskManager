using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Domain.Entities.Shared;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities.Assignments
{
    public class AssignmentLog : MongoEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string AssignmentId { get; set; }

        public required OperationTypeEnum OperationType { get; set; }

        public required DateTime OperationDate { get; set; }

        public required Assignment AssignmentState { get; set; }
    }
}
