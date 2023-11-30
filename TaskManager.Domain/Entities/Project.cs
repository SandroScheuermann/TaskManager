using MongoDB.Bson;
using TaskManager.Domain.Entities.Shared;

namespace TaskManager.Domain.Entities
{
    public class Project : MongoEntity
    {  
        public required List<ObjectId> Assignments { get; set; }

        public required ObjectId UserId { get; set; }
    }
}
