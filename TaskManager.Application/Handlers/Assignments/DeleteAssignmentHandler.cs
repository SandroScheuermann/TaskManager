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
    public class DeleteAssignmentHandler(IAssignmentRepository assignmentRepository, IValidator<DeleteAssignmentRequest> assignmentValidator)
        : IRequestHandler<DeleteAssignmentCommand, Result<DeleteAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<DeleteAssignmentRequest> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<DeleteAssignmentResponse, Error>> Handle(DeleteAssignmentCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request)
                .Bind(CheckAssignmentExistance)
                .Bind(DeleteAssignment);

            return Task.FromResult(response);
        }

        private Result<DeleteAssignmentRequest, Error> ValidateRequest(DeleteAssignmentRequest request)
        {
            var validationResult = AssignmentValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<DeleteAssignmentRequest, Error> CheckAssignmentExistance(DeleteAssignmentRequest request)
        {
            var assignmentExists = AssignmentRepository.CheckIfExistsById(request.Id).Result;

            if (!assignmentExists)
            {
                return new AssignmentDoesntExistError();
            }

            return request;
        }
        private Result<DeleteAssignmentResponse, Error> DeleteAssignment(DeleteAssignmentRequest request)
        {
            var result = AssignmentRepository.DeleteAsync(request.Id).Result;

            return result.DeletedCount > 0 ? new DeleteAssignmentResponse { } : new UnknownError("Failed to delete the informed assignment.");
        }
    }
}
