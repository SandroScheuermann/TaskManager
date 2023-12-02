using TaskManager.Domain.Entities.Shared;

namespace TaskManager.Domain.Entities
{
    public class Project : MongoEntity
    {
        public required string? UserId { get; set; } 

        public required string? ProjectName { get; set; }
    }
}
