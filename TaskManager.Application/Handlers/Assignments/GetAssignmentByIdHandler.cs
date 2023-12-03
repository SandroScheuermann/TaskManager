using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Assignments
{
    public class GetAssignmentByIdHandler(IAssignmentRepository assignmentRepository, IValidator<GetAssignmentByIdCommand> assignmentValidator)
        : IRequestHandler<GetAssignmentByIdCommand, Result<GetAssignmentByIdResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<GetAssignmentByIdCommand> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<GetAssignmentByIdResponse, Error>> Handle(GetAssignmentByIdCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(GetAssignment); 

            return Task.FromResult(response);
        }

        private Result<GetAssignmentByIdCommand, Error> ValidateRequest(GetAssignmentByIdCommand command)
        {
            var validationResult = AssignmentValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<GetAssignmentByIdResponse, Error> GetAssignment(GetAssignmentByIdCommand command)
        {
            var assignment = AssignmentRepository.GetByIdAsync(command.Id).Result;

            return assignment is not null ? new GetAssignmentByIdResponse { Assignment = assignment } : new AssignmentDoesntExistError(); 
        }
    }
}
