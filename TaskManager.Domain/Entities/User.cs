using MongoDB.Bson;
using TaskManager.Domain.Entities.Shared;

namespace TaskManager.Domain.Entities
{
    public class User : MongoEntity
    { 
        public required string? UserName { get; set; }
    }
}
