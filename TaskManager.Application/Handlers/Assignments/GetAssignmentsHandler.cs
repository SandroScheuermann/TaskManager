using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Assignments
{
    public class GetAssignmentsHandler(IAssignmentRepository assignmentRepository) : IRequestHandler<GetAssignmentsCommand, Result<GetAssignmentsResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository; 

        public Task<Result<GetAssignmentsResponse, Error>> Handle(GetAssignmentsCommand command, CancellationToken cancellationToken)
        {
            var response = GetAssignments();

            return Task.FromResult(response);
        }

        private Result<GetAssignmentsResponse, Error> GetAssignments()
        {
            var assignments = AssignmentRepository.GetAllAsync().Result;

            var response = new GetAssignmentsResponse()
            {
                Assignments = assignments,
            };

            return response;
        }  
    }
}
