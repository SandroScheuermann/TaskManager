using MongoDB.Bson;
using TaskManager.Domain.Entities.Shared;

namespace TaskManager.Domain.Entities
{
    public class Project : MongoEntity
    {  
        public List<string> Assignments { get; set; } = [];

        public string? UserId { get; set; }
    }
}
