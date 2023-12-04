using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Application.Handlers.Assignments
{
    public class UpdateAssignmentHandler(
        IAssignmentRepository assignmentRepository, 
        IValidator<UpdateAssignmentCommand> assignmentValidator,
        IMediator mediator) : IRequestHandler<UpdateAssignmentCommand, Result<UpdateAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository; 
        public IValidator<UpdateAssignmentCommand> AssignmentValidator { get; set; } = assignmentValidator; 
        public IMediator Mediator { get; set; } = mediator; 

        public Task<Result<UpdateAssignmentResponse, Error>> Handle(UpdateAssignmentCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(CheckAssignmentExistance)
                .Bind(UpdateAssignment);

            return Task.FromResult(response);
        } 

        private Result<UpdateAssignmentCommand, Error> ValidateRequest(UpdateAssignmentCommand command)
        {
            var validationResult = AssignmentValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<UpdateAssignmentCommand, Error> CheckAssignmentExistance(UpdateAssignmentCommand command)
        {
            var assignmentExists = AssignmentRepository.CheckExistanceById(command.Id).Result;

            if (!assignmentExists)
            {
                return new AssignmentNotFoundError();
            }

            return command;
        }
        private Result<UpdateAssignmentResponse, Error> UpdateAssignment(UpdateAssignmentCommand command)
        {
            var updatedAssignment = new Assignment()
            {
                Id = command.Id,
                ProjectId = null,
                Title = null,
                Description = command.Request.Description,
                ExpirationDate = null,
                Status = command.Request.Status, 
                Priority = null,
                Comments = null,
            };

            var updateResult = AssignmentRepository.UpdateAsync(updatedAssignment).Result;

            if (updateResult.ModifiedCount < 0)
            {
                return new UnknownError();
            } 

            var response = new UpdateAssignmentResponse()
            {
                Id = updatedAssignment.Id, 
            };

            return response;
        }
    }
}
