using TaskManager.Domain.Entities.Shared;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Assignment : MongoEntity
    {
        public required string? ProjectId { get; set; }

        public required string? Title { get; set; }

        public required string? Description { get; set; }

        public required DateTime? ExpireDate { get; set; }

        public required AssignmentStatus? Status { get; set; }
    }
}
