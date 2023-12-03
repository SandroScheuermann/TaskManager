using TaskManager.Domain.Entities.Assignments;

namespace TaskManager.Application.Responses.Assignments
{
    public class GetAssignmentByIdResponse
    {
        public required Assignment? Assignment { get; set; }
    }
}
