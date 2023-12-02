using MongoDB.Bson;

namespace TaskManager.Application.Requests.Assignments
{
    public class GetAssignmentByIdRequest
    {
        public required string Id { get; set; }
    }
}
