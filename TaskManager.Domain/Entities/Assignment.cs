using TaskManager.Domain.Entities.Shared;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Assignment : MongoEntity
    {  
        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime ExpireDate { get; set; }

        public AssignmentStatus Status { get; set; }
    }
}
