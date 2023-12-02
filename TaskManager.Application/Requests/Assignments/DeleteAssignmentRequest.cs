using MongoDB.Bson;

namespace TaskManager.Application.Requests.Assignments
{
    public class DeleteAssignmentRequest
    {
        public required string Id { get; set; }
    }
}
