using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Assignments
{
    public class GetAssignmentsByIdHandler(IAssignmentRepository assignmentRepository, IValidator<GetAssignmentByIdRequest> assignmentValidator)
        : IRequestHandler<GetAssignmentByIdCommand, Result<GetAssignmentByIdResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<GetAssignmentByIdRequest> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<GetAssignmentByIdResponse, Error>> Handle(GetAssignmentByIdCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request)
                .Bind(GetAssignment); 

            return Task.FromResult(response);
        }

        private Result<GetAssignmentByIdRequest, Error> ValidateRequest(GetAssignmentByIdRequest request)
        {
            var validationResult = AssignmentValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<GetAssignmentByIdResponse, Error> GetAssignment(GetAssignmentByIdRequest request)
        {
            var assignment = AssignmentRepository.GetByIdAsync(request.Id).Result;

            return assignment is not null ? new GetAssignmentByIdResponse { Assignment = assignment } : new AssignmentDoesntExistError(); 
        }
    }
}
